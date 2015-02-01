using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppCreator.Controls {
	public class BindablePicker<T> : Picker {
		public BindablePicker() {
			SelectedIndexChanged += OnSelectedIndexChanged;
		}

		public IEnumerable<T> ItemsSource { get; set; }
		public T SelectedItem { get; set; }

		private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue) {
			var picker = bindable as BindablePicker<T>;
			picker.Items.Clear();

			if (newvalue != null) {
				foreach (var item in newvalue) {
					picker.Items.Add(item.ToString());
				}
			}
		}

		private void OnSelectedIndexChanged(object sender, EventArgs eventArgs) {
			if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1) {
				SelectedItem = default(T);
			} else {
				SelectedItem = ItemsSource.ElementAt(SelectedIndex);
			}
		}
		private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue) {
			var picker = bindable as BindablePicker<T>;

			if (newvalue != null)
				picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
		}
	}
}

