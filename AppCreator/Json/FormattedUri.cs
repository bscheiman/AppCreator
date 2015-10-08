#define DEBUG

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AppCreator.Json {
	public class FormattedUri {
		public string Uri { get; set; }
		public Dictionary<string, string> QueryString { get; set; }
		public object Parameters { get; set; }
		internal static Uri BaseAddress { get; set; }

		public FormattedUri(string uri) {
			Uri = uri;
		}

		public FormattedUri(string uri, object urlParameters) {
			Uri = uri;
			Parameters = urlParameters;
		}

		public FormattedUri(string uri, Dictionary<string, string> queryString) : this(uri) {
			QueryString = queryString;
		}

		public FormattedUri(string uri, object urlParameters, Dictionary<string, string> queryString) : this(uri,
			                                                                                                    urlParameters) {
			QueryString = queryString;
		}

		public override string ToString() {
			return (string) this;
		}

		public static implicit operator string(FormattedUri uri) {
			var isAbsolute = uri.Uri.StartsWith("http");
			var queryString = uri.QueryString == null ? "" : string.Join("&", uri.QueryString.Select(m => string.Format("{0}={1}", m.Key, System.Uri.EscapeUriString(m.Value))));

			var qs = string.IsNullOrEmpty(queryString) ? "" : "?";
			var url = isAbsolute ? string.Format("{0}{1}{2}", uri.Uri, qs, queryString) : string.Format("{0}{1}{2}{3}",
			                                                                                            BaseAddress,
			                                                                                            uri.Uri,
			                                                                                            qs,
			                                                                                            queryString);

			if (uri.Parameters != null) {
				var type = uri.Parameters.GetType();
				var properties = type.GetRuntimeProperties();

				foreach (var t in properties) {
					var propValue = t.GetValue(uri.Parameters);
					var value = propValue == null ? "" : propValue.ToString();

					url = url.Replace(string.Format(":{0}", t.Name), value);
					url = url.Replace(string.Format("{{{0}}}", t.Name), value);
				}
			}

			return url;
		}

		public static implicit operator FormattedUri(string uri) {
			return new FormattedUri(uri);
		}
	}
	
}
