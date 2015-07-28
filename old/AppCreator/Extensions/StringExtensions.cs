#region
using Newtonsoft.Json;

#endregion

namespace AppCreator.Extensions {
    public static class StringExtensions {
        public static T FromJson<T>(this string str) {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}