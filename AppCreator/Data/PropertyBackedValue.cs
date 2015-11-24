using System;
using Xamarin.Forms;

namespace AppCreator.Data {
	public class PropertyBackedValue<T> {
		internal string Key { get; set; }
		internal bool Found { get; set; }
		internal T Value { get; set; }

		public PropertyBackedValue(string key, T val = default(T)) {
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException(nameof(key));

			Key = key;
			Found = Application.Current.Properties.ContainsKey(key);

			Value = Found ? (T) Application.Current.Properties[key] : val;

			Application.Current.SavePropertiesAsync();
		}

		public T Get() {
			return Value;
		}

		public void Set(T val) {
			Value = val;
			Application.Current.SavePropertiesAsync();
		}

		public static implicit operator T(PropertyBackedValue<T> tpb) {
			return tpb.Get();
		}
	}
}

