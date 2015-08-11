using System;
using System.Threading.Tasks;
using System.Net.Http;
using AppCreator.Extensions;
using System.Collections.Generic;
using System.Text;
using ModernHttpClient;
using System.Linq;
using System.Reflection;

namespace AppCreator {
	public abstract class BaseJsonClient {
		internal Uri BaseAddress { get; set; }

		public BaseJsonClient(string baseAddress) {
			BaseAddress = new Uri(baseAddress);
		}

		protected async Task<T> Post<T>(string url, object obj = null) {
			using (var httpClient = new HttpClient(new NativeMessageHandler())) {
				var res = await httpClient.PostAsync(GetUrl(url), new StringContent(obj.ToJson(), Encoding.UTF8, "application/json"));
				var msg = await res.Content.ReadAsStringAsync();

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> Post<T>(string url, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			using (var httpClient = new HttpClient(new NativeMessageHandler())) {
				var content = new MultipartFormDataContent();

				foreach (var k in keyValues)
					content.Add(new StringContent(k.Value), k.Key);

				var res = await httpClient.PostAsync(GetUrl(url), content);
				var msg = await res.Content.ReadAsStringAsync();

				return msg.FromJson<T>();
			}
		}

		protected async Task<T> Get<T>(string url, Dictionary<string, string> keyValues = null) {
			keyValues = keyValues ?? new Dictionary<string, string>();

			using (var httpClient = new HttpClient(new NativeMessageHandler())) {
				var fullUrl = GetUrl(url, string.Join("&", keyValues.Select(m => string.Format("{0}={1}", m.Key, m.Value))));
				var res = await httpClient.GetAsync(fullUrl).ConfigureAwait(false);
				var msg = await res.Content.ReadAsStringAsync();

				return msg.FromJson<T>();
			}
		}

		private FormUrlEncodedContent ObjectToFormContent(object obj) {
			if (obj == null)
				return new List<KeyValuePair<string, string>>();

			var type = obj.GetType();
			var properties = type.GetRuntimeProperties();

			return new FormUrlEncodedContent(properties.Select(p => new KeyValuePair<string, string>(p.Name,
			                                                                                         p.GetValue(obj).ToString())).ToList());
		}

		private Uri FormatAddressWithObject(string url, object obj) {
			if (obj == null)
				return url;

			var type = obj.GetType();
			var properties = type.GetRuntimeProperties();

			foreach (var p in properties)
				url = url.Replace(string.Format("{{{0}}}", p.Name), p.GetValue(obj).ToString());

			return url;
		}

		private Uri GetUrl(string url, string query = "") {
			var isAbsolute = url.StartsWith("http");

			if (isAbsolute)
				return new Uri(string.Format("{0}{1}{2}", url, string.IsNullOrEmpty(query) ? "" : "?", query));

			return new Uri(string.Format("{0}{1}{2}{3}", BaseAddress, url, string.IsNullOrEmpty(query) ? "" : "?", query));
		}
	}
}

