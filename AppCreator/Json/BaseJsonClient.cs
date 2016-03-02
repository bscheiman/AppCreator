using System;
using System.Threading.Tasks;
using System.Net.Http;
using AppCreator.Extensions;
using System.Collections.Generic;
using System.Text;
using ModernHttpClient;
using System.Linq;
using System.Reflection;
using System.Net;
using System.Globalization;
using AppCreator.Helpers;
using System.Diagnostics;
using Polly;
using AppCreator.Exceptions;
using Splat;
using Fusillade;

namespace AppCreator.Json {
	public enum RequestType {
		Speculative,
		Background,
		User
	}

	public abstract class BaseJsonClient {
		internal static Random Random = new Random();
		internal bool ReturnNull { get; set; }

		static BaseJsonClient() {
			Locator.CurrentMutable.RegisterConstant(new NativeMessageHandler(), typeof(HttpMessageHandler));
		}

		protected BaseJsonClient(string baseAddress, bool returnNull) {
			FormattedUri.BaseAddress = new Uri(baseAddress);
			ReturnNull = returnNull;
		}

		protected abstract void PreExecution();
		protected abstract void PostExecution();
		protected abstract bool CanRun();

		protected async virtual Task<T> ReadJson<T>(HttpResponseMessage message) where T : new() {
			try {
				return (await message.Content.ReadAsStringAsync()).FromJson<T>();
			} catch (FormatException ex) {
				HandleException(string.Empty, ex);

				Util.Log("--- INVALID JSON ---");

				return ReturnNull ? default(T) : new T();
			} catch (Exception ex) {
				HandleException(string.Empty, ex);

				Util.Log("--- EXT. SERVICE ERROR ---");

				return ReturnNull ? default(T) : new T();
			}
		}

		public void ResetCache() {
			NetCache.Speculative.ResetLimit(1048576 * 5 /*MB*/);
		}

		protected virtual void HandleException(string id, Exception ex) {
			Util.Log("HandleException: {0}", ex.ToString());
		}

		protected virtual Policy GetPolicy() {
			return Policy.Handle<Exception>().WaitAndRetryAsync(
				1, 
				s => TimeSpan.FromSeconds(Math.Pow(2, s)),
				(e, t) => { }
			);
		}

		protected virtual HttpClient ModifyClient(HttpClient client, HttpMethod method) {
			return client;
		}

		protected virtual HttpRequestMessage ModifyMessage(HttpRequestMessage message, HttpMethod method) {
			return message;
		}

		protected virtual HttpMessageHandler ModifyHandler(HttpMessageHandler handler, HttpMethod method) {
			return handler;
		}

		protected virtual async Task<T> WrapRequests<T>(Func<Task<T>> func)  {
			return (await func());
		}

		protected HttpMessageHandler GetHandler(HttpMethod method, RequestType requestType) {
			HttpMessageHandler handler = requestType == RequestType.User ? NetCache.UserInitiated : requestType == RequestType.Background ? NetCache.Background : NetCache.Speculative;

			return ModifyHandler(handler, method);
		}

		private string GenerateId() {
			var sb = new StringBuilder();

			for (int i = 0; i < 6; i++)
				sb.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65))));

			return sb.ToString();
		}

		protected async Task<HttpResponseMessage> Send<T>(HttpMethod method, FormattedUri uri, HttpContent content = null, RequestType requestType = RequestType.User) where T : new() {
			var id = GenerateId();

			try {
				PreExecution();

				if (!CanRun()) {
					Util.Log("Can't run...");

					throw new CantExecuteException();
				}

				var policy = GetPolicy();

				return await policy.ExecuteAsync(async () => {
					using (var httpClient = ModifyClient(new HttpClient(GetHandler(method, requestType)), method)) {
						var finalUri = uri.ToString();
						var sw = Stopwatch.StartNew();

						Util.Log("{0} / {1} / TX / URL: {2}", method.Method.ToUpper(), id, finalUri);
						Util.Log("{0} / {1} / TX / PLD: {2}", method.Method.ToUpper(), id, content == null ? "--EMPTY--" : await content.ReadAsStringAsync());

						var req = new HttpRequestMessage(method, finalUri);

						if (content != null)
							req.Content = content;

						var msg = await ModifyClient(httpClient, method).SendAsync(ModifyMessage(req, method)).ConfigureAwait(false);

						sw.Stop();

						Util.Log("{0} / {1} / RX / {2}ms: {3}", method.Method.ToUpper(), id, sw.ElapsedMilliseconds.ToString("N0", new CultureInfo("en-US")), await msg.Content.ReadAsStringAsync());

						return msg;
					}
				});
			} catch (Exception ex) {
				HandleException(id, ex);

				Util.Log("Exception: " + ex);
				
				return new HttpResponseMessage { Content = new StringContent(new T().ToJson()) };
			} finally {
				PostExecution();
			}
		}

		protected async Task<int> Status(HttpMethod method, FormattedUri uri) {
			return CanRun() ? (int) (await Send<int>(method, uri)).StatusCode : -1;
		}

		protected async Task<int> Status(FormattedUri uri) {
			return CanRun() ? (int) (await Send<int>(HttpMethod.Get, uri)).StatusCode : -1;
		}

		protected async Task<int> Status(HttpMethod method, string uri, object parameters) {
			return await Status(method, new FormattedUri(uri, parameters));
		}

		protected async Task<int> Status(string uri, object parameters) {
			return await Status(new FormattedUri(uri, parameters));
		}

		protected async Task<T> Post<T>(FormattedUri uri, object postedObject = null) where T : new() {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(HttpMethod.Post, uri, new StringContent(postedObject.ToJson(),
			                                                                Encoding.UTF8,
			                                                                "application/json"));

			return await ReadJson<T>(res);
		}

		protected Task<T> Post<T>(string uri, object urlParameters, object postedObject = null) where T : new() {
			return Post<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

		protected async Task<T> Patch<T>(FormattedUri uri, object postedObject = null) where T : new() {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(new HttpMethod("PATCH"), uri, new StringContent(postedObject.ToJson(),
			                                                                        Encoding.UTF8,
			                                                                        "application/json"));
			return await ReadJson<T>(res);
		}

		protected Task<T> Patch<T>(string uri, object urlParameters, object postedObject = null) where T : new() {
			return Patch<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

        protected Task<T> PatchFormDataContent<T>(string uri, object urlParameters, Dictionary<string, string> keyValues = null) where T : new() {
            return PatchFormDataContent<T>(new FormattedUri(uri, urlParameters), keyValues);
        }

        protected async Task<T> PatchFormDataContent<T>(FormattedUri uri, Dictionary<string, string> keyValues = null) where T : new() {
			keyValues = keyValues ?? new Dictionary<string, string>();

			var res = await Send<T>(new HttpMethod("PATCH"), uri, new FormUrlEncodedContent(keyValues.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))));

			return await ReadJson<T>(res);
		}

		protected async Task<T> Put<T>(FormattedUri uri, object postedObject = null) where T : new() {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(HttpMethod.Put, uri, new StringContent(postedObject.ToJson(),
			                                                               Encoding.UTF8,
			                                                               "application/json"));
			return await ReadJson<T>(res);
		}

		protected Task<T> Put<T>(string uri, object urlParameters, object postedObject = null) where T : new() {
			return Put<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

		protected Task<T> Get<T>(string uri, object parameters, RequestType requestType = RequestType.User) where T : new() {
			return Get<T>(new FormattedUri(uri, parameters), requestType);
		}

		protected Task<T> Get<T>(string uri, Dictionary<string, string> keyValues, RequestType requestType = RequestType.User) where T : new() {
			return Get<T>(new FormattedUri(uri, keyValues), requestType);
		}

		protected async Task<T> Get<T>(FormattedUri uri, RequestType requestType = RequestType.User) where T : new() {
			var res = await Send<T>(HttpMethod.Get, uri, null, requestType);

			return await ReadJson<T>(res);
		}

		protected async Task<T> Delete<T>(FormattedUri uri) where T : new() {
			var res = await Send<T>(HttpMethod.Delete, uri);

			return await ReadJson<T>(res);
		}

		protected Task<T> Delete<T>(string uri, object urlParameters) where T : new() {
			return Delete<T>(new FormattedUri(uri, urlParameters));
		}

		protected async Task<T> PostFormDataContent<T>(FormattedUri uri, Dictionary<string, string> keyValues = null) where T : new() {
			keyValues = keyValues ?? new Dictionary<string, string>();

			var res = await Send<T>(HttpMethod.Post, uri, new FormUrlEncodedContent(keyValues.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))));

			return await ReadJson<T>(res);
		}

		protected async Task<T> PutFormDataContent<T>(FormattedUri uri, Dictionary<string, string> keyValues = null) where T : new() {
			keyValues = keyValues ?? new Dictionary<string, string>();

			var res = await Send<T>(HttpMethod.Post, uri, new FormUrlEncodedContent(keyValues.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))));

			return await ReadJson<T>(res);
		}

		protected FormUrlEncodedContent ObjectToFormContent(object obj) {
			if (obj == null)
				return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());

			var type = obj.GetType();
			var properties = type.GetRuntimeProperties();

			return new FormUrlEncodedContent(properties.Select(p => new KeyValuePair<string, string>(p.Name,
			                                                                                         p.GetValue(obj).ToString())).ToList());
		}
	}
}

