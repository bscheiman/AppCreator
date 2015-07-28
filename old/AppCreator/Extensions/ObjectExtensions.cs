#region
using Newtonsoft.Json;

#endregion

namespace AppCreator.Extensions {
    public static class ObjectExtensions {
        public static string ToJson(this object obj, JsonSerializerSettings settings = null) {
            return settings == null ? JsonConvert.SerializeObject(obj) : JsonConvert.SerializeObject(obj, settings);
        }
    }
}