#region
using System;
using System.Threading.Tasks;

#endregion

namespace AppCreator.Json {
    public static class JsonHelper {
        public static Task<TReturn> Delete<TReturn>(string cmd, string queryString = null) where TReturn : BaseJsonObject, new() {
            using (var client = new HttpClient(new NativeMessageHandler()))
                return Send<TReturn>(client, HttpMethod.Delete, cmd, queryString);
        }

        public static Task<TReturn> Get<TReturn>(string cmd, string queryString = null) where TReturn : BaseJsonObject, new() {
            using (var client = new HttpClient(new NativeMessageHandler()))
                return Send<TReturn>(client, HttpMethod.Get, cmd, queryString);
        }

        public static Task<TReturn> Post<TReturn>(string cmd, string queryString = null, object post = null)
            where TReturn : BaseJsonObject, new() {
            using (var client = new HttpClient(new NativeMessageHandler()))
                return Send<TReturn>(client, HttpMethod.Post, cmd, queryString, post);
        }

        public static Task<TReturn> Put<TReturn>(string cmd, string queryString = null, object post = null)
            where TReturn : BaseJsonObject, new() {
            using (var client = new HttpClient(new NativeMessageHandler()))
                return Send<TReturn>(client, HttpMethod.Put, cmd, queryString, post);
        }

        internal static async Task<TReturn> Send<TReturn>(HttpClient client, HttpMethod method, string url, object queryString = null,
                                                          object post = null) where TReturn : BaseJsonObject, new() {
            try {
                string uri = url;

                if (queryString is string)
                    uri += "?" + queryString;
//				else if (queryString != null)
//					uri += queryString.ToQueryString();

                // TODO: Add headers
                //client.DefaultRequestHeaders.Add("", "");

                string jsonResponse;
                HttpResponseMessage response = null;

                switch (method.Method.ToUpper()) {
                    case "DELETE":
                        response = await client.DeleteAsync(uri);

                        break;

                    case "GET":
                        response = await client.GetAsync(uri);

                        break;

                    case "POST":
//						response = await client.PostAsync(uri, new StringContent(post.ToJson() ?? "", Encoding.UTF8, "application/json"));

                        break;

                    case "PUT":
//						response = await client.PutAsync(uri, new StringContent(post.ToJson() ?? "", Encoding.UTF8, "application/json"));

                        break;

                    default:
                        throw new ArgumentException("Unsupported method");
                }

                jsonResponse = await response.Content.ReadAsStringAsync();

                //return string.IsNullOrEmpty(jsonResponse) ? default(TReturn) : jsonResponse.FromJson<TReturn>();
                return default(TReturn);
            } catch (Exception ex) {
                return new TReturn {
                    Error = true,
                    Exception = ex
                };
            }
        }
    }
}