#define DEBUG

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
using System.Diagnostics;

namespace AppCreator.Json {
	public abstract class BaseJsonClient {
		internal bool DebugMode { get; set; }

		protected BaseJsonClient(string baseAddress, bool debug = false) {
			FormattedUri.BaseAddress = new Uri(baseAddress);
			DebugMode = debug;
		}

		protected abstract void PreExecution();
		protected abstract void PostExecution();
		protected abstract bool CanRun();
		protected abstract void HandleException(Exception ex);

		protected virtual HttpClient ModifyClient(HttpClient client, HttpMethod method) {
			return client;
		}

		protected virtual HttpRequestMessage ModifyMessage(HttpRequestMessage message, HttpMethod method) {
			return message;
		}

		protected virtual HttpMessageHandler ModifyHandler(HttpMessageHandler handler, HttpMethod method) {
			return handler;
		}

		protected HttpMessageHandler GetHandler(HttpMethod method) {
			return ModifyHandler(new NativeMessageHandler {
				AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
			}, method);
		}

		protected async Task<HttpResponseMessage> Send<T>(HttpMethod method, FormattedUri uri, HttpContent content = null) {
			try {
				PreExecution();

				if (!CanRun()) {
					if (DebugMode)
						Debug.WriteLine("Can't run...");
					
					return new HttpResponseMessage { Content = new StringContent("can't run") };
				}

				using (var httpClient = ModifyClient(new HttpClient(GetHandler(method)), method)) {
					var finalUri = uri.ToString();

					if (DebugMode) {
						Debug.WriteLine("{0} / TX / URL: {1}", method.Method.ToUpper(), finalUri);
						Debug.WriteLine("{0} / TX / PLD: {1}", method.Method.ToUpper(), content == null ? "--EMPTY--" : await content.ReadAsStringAsync());
					}

					var req = new HttpRequestMessage(method, finalUri);

					if (content != null)
						req.Content = content;

					var msg = await ModifyClient(httpClient, method).SendAsync(ModifyMessage(req, method)).ConfigureAwait(false);

					if (DebugMode)
						Debug.WriteLine("{0} / RX: {1}", method.Method.ToUpper(), await msg.Content.ReadAsStringAsync());

					return msg;
				}
			} catch (Exception ex) {
				HandleException(ex);

				if (DebugMode)
					Debug.WriteLine("Exception: " + ex);
				
				return new HttpResponseMessage { Content = new StringContent("exception occurred") };
			} finally {
				PostExecution();
			}
		}

		protected async Task<int> Status(FormattedUri uri) {
			return CanRun() ? (int) (await Send<int>(HttpMethod.Get, uri)).StatusCode : -1;
		}

		protected async Task<int> Status(string uri, object parameters) {
			return await Status(new FormattedUri(uri, parameters));
		}

		protected async Task<T> Post<T>(FormattedUri uri, object postedObject = null) {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(HttpMethod.Post, uri, new StringContent(postedObject.ToJson(),
			                                                                Encoding.UTF8,
			                                                                "application/json"));
			var msg = await res.Content.ReadAsStringAsync();


			return msg.FromJson<T>();
		}

		protected Task<T> Post<T>(string uri, object urlParameters, object postedObject = null) {
			return Post<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

		protected async Task<T> Patch<T>(FormattedUri uri, object postedObject = null) {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(new HttpMethod("PATCH"), uri, new StringContent(postedObject.ToJson(),
			                                                                        Encoding.UTF8,
			                                                                        "application/json"));
			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
		}

		protected Task<T> Patch<T>(string uri, object urlParameters, object postedObject = null) {
			return Patch<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

		protected async Task<T> Put<T>(FormattedUri uri, object postedObject = null) {
			postedObject = postedObject ?? new object();

			var res = await Send<T>(HttpMethod.Put, uri, new StringContent(postedObject.ToJson(),
			                                                               Encoding.UTF8,
			                                                               "application/json"));
			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
		}

		protected Task<T> Put<T>(string uri, object urlParameters, object postedObject = null) {
			return Put<T>(new FormattedUri(uri, urlParameters), postedObject);
		}

		protected Task<T> Get<T>(string uri, object parameters) {
			return Get<T>(new FormattedUri(uri, parameters));
		}

		protected Task<T> Get<T>(string uri, Dictionary<string, string> keyValues) {
			return Get<T>(new FormattedUri(uri, keyValues));
		}

		protected async Task<T> Get<T>(FormattedUri uri) {
			var res = await Send<T>(HttpMethod.Get, uri);

			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
		}

		protected async Task<T> Delete<T>(FormattedUri uri) {
			var res = await Send<T>(HttpMethod.Delete, uri);

			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
		}

		protected Task<T> Delete<T>(string uri, object urlParameters) {
			return Delete<T>(new FormattedUri(uri, urlParameters));
		}

		protected async Task<T> PostFormDataContent<T>(FormattedUri uri, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			var res = await Send<T>(HttpMethod.Post, uri, new FormUrlEncodedContent(keyValues.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))));

			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
		}

		protected async Task<T> PutFormDataContent<T>(FormattedUri uri, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			var res = await Send<T>(HttpMethod.Post, uri, new FormUrlEncodedContent(keyValues.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))));

			return (await res.Content.ReadAsStringAsync()).FromJson<T>();
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

