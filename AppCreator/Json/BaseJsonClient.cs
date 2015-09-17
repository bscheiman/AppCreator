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
		protected Uri BaseAddress { get; set; }
		internal bool DebugMode { get; set; }

		protected BaseJsonClient(string baseAddress, bool debug = false) {
			BaseAddress = new Uri(baseAddress);
			DebugMode = debug;
		}

		protected abstract void ModifyHandler(HttpMessageHandler handler, HttpMethod method);

		protected HttpMessageHandler GetHandler(HttpMethod method) {
			var handler = new NativeMessageHandler {
				AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
			};

			ModifyHandler(handler, method);

			return handler;
		}

		protected async Task<T> Post<T>(string url, object obj = null) {
			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Post))) {
				var json = obj.ToJson();

				if (DebugMode)
					Debug.WriteLine("POST / TX: {0}", json);
				
				var res = await httpClient.PostAsync(GetUrl(url),
				                                     new StringContent(json,
				                                                       Encoding.UTF8,
				                                                       "application/json"));
				var msg = await res.Content.ReadAsStringAsync();

				if (DebugMode)
					Debug.WriteLine("POST / RX: {0}", msg);

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> Put<T>(string url, object obj = null) {
			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Put))) {
				var json = obj.ToJson();

				if (DebugMode)
					Debug.WriteLine("PUT / TX: {0}", json);

				var res = await httpClient.PutAsync(GetUrl(url),
				                                    new StringContent(json,
				                                                      Encoding.UTF8,
				                                                      "application/json"));
				var msg = await res.Content.ReadAsStringAsync();

				if (DebugMode)
					Debug.WriteLine("PUT / RX: {0}", msg);
				
				return msg.FromJson<T>();
			}
		}

		protected async Task<T> Get<T>(string url, Dictionary<string, string> keyValues = null) {
			if (keyValues == null)
				keyValues = new Dictionary<string, string>();

			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Get))) {
				var parms = string.Join("&", keyValues.Select(m => string.Format("{0}={1}", m.Key, Uri.EscapeUriString(m.Value))));
				var fullUrl = GetUrl(url, parms);

				if (DebugMode)
					Debug.WriteLine("GET / TX: {0}", fullUrl);

				var res = await httpClient.GetAsync(fullUrl).ConfigureAwait(false);
				var msg = await res.Content.ReadAsStringAsync();

				if (DebugMode)
					Debug.WriteLine("GET / RX: {0}", msg);

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> Delete<T>(string url, Dictionary<string, string> keyValues = null) {
			if (keyValues == null)
				keyValues = new Dictionary<string, string>();

			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Get))) {
				var parms = string.Join("&", keyValues.Select(m => string.Format("{0}={1}", m.Key, Uri.EscapeUriString(m.Value))));
				var fullUrl = GetUrl(url, parms);

				if (DebugMode)
					Debug.WriteLine("DELETE / TX: {0}", fullUrl);

				var res = await httpClient.DeleteAsync(fullUrl).ConfigureAwait(false);
				var msg = await res.Content.ReadAsStringAsync();

				if (DebugMode)
					Debug.WriteLine("DELETE / RX: {0}", msg);

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> PostFormDataContent<T>(string url, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Post))) {
				var content = new MultipartFormDataContent();

				foreach (var k in keyValues) {
					content.Add(new StringContent(k.Value), k.Key);

					if (DebugMode)
						Debug.WriteLine("POST / TXFORM: {0} - {1}", k.Key, k.Value);
				}


				var res = await httpClient.PostAsync(GetUrl(url), content);
				var msg = await res.Content.ReadAsStringAsync();

				if (DebugMode)
					Debug.WriteLine("POST / RXFORM: {0}", msg);

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> PutFormDataContent<T>(string url, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			using (var httpClient = new HttpClient(GetHandler(HttpMethod.Put))) {
				var content = new MultipartFormDataContent();

				foreach (var k in keyValues)
					content.Add(new StringContent(k.Value), k.Key);

				var res = await httpClient.PutAsync(GetUrl(url), content);
				var msg = await res.Content.ReadAsStringAsync();

				return msg.FromJson<T>();
			}
		}

		protected FormUrlEncodedContent ObjectToFormContent(object obj) {
			if (obj == null)
				return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());

			var type = obj.GetType();
			var properties = type.GetRuntimeProperties();

			return new FormUrlEncodedContent(properties.Select(p => new KeyValuePair<string, string>(p.Name,
			                                                                                         p.GetValue(obj).ToString())).ToList());
		}

		protected Uri GetUrl(string url, string query = "") {
			var isAbsolute = url.StartsWith("http");

			return isAbsolute 
					? new Uri(string.Format("{0}{1}{2}",
			                          url,
			                          string.IsNullOrEmpty(query) ? "" : "?",
			                          query)) 
					: new Uri(string.Format("{0}{1}{2}{3}",
			                          BaseAddress,
			                          url,
			                          string.IsNullOrEmpty(query) ? "" : "?",
			                          query));

		}
	}
}

