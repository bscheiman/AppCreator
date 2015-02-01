using System;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

namespace AppCreator.Controls {
	public class BindablePicker<T> : Picker {
		public static BindableProperty ItemsSourceProperty = BindableProperty.Create<BindablePicker<T>, IEnumerable>(o => o.ItemsSource,
			                                                     default(IEnumerable), propertyChanged: OnItemsSourceChanged);

		public static BindableProperty SelectedItemProperty = BindableProperty.Create<BindablePicker<T>, object>(o => o.SelectedItem,
			                                                      default(object), propertyChanged: OnSelectedItemChanged);


		public IEnumerable<T> ItemsSource {
			get { return (IEnumerable<T>)GetValue(ItemsSourceProperty); }
			set {
				var notifyChanged = value as INotifyCollectionChanged;

				if (notifyChanged != null) {
					var obj = notifyChanged;

					obj.CollectionChanged += (sender, e) => {
						Items.Clear();

						foreach (var i in (value as ICollection))
							Items.Add(i.ToString());
					};
				}

				SetValue(ItemsSourceProperty, value);
			}
		}

		public T SelectedItem {
			get { return (T)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public BindablePicker() {
			SelectedIndexChanged += OnSelectedIndexChanged;
		}

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

