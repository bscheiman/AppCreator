using System;
using Xamarin.Forms;
using AppCreator.Helpers;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace AppCreator.Data {
	public class PropertyBackedValue<T> {
		internal string Key { get; set; }
		internal T Value { get; set; }

		static PropertyBackedValue() {
		}

		public PropertyBackedValue(string key, T val = default(T)) {
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException(nameof(key));

			Key = key;
			Value = CrossSettings.Current.GetValueOrDefault(key, val);

			Util.Log("Key: {0}, Value: {1}", Key, Value);
		}

		public T Get() {
			return Value;
		}

		public void Set(T val) {
			Value = val;
			CrossSettings.Current.AddOrUpdateValue(Key, Value);
			Util.Log("Key {0}, new value: {1}", Key, Value);
		}

		public static implicit operator T(PropertyBackedValue<T> tpb) {
			return tpb.Get();
		}
	}
}

