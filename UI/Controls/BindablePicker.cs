#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Controls {
    public class BindablePicker : BindablePicker<object> {
    }

    [ImplementPropertyChanged]
    public class BindablePicker<T> : Picker where T : class {
        public IEnumerable<T> ItemsSource {
            get { return (IEnumerable<T>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public T SelectedItem {
            get { return (T) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public BindablePicker() {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        internal void RaisePropertyChanged(string propName) {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable value, IEnumerable newValue) {
            var picker = (BindablePicker<T>) bindable;
            var notifyCollection = newValue as INotifyCollectionChanged;

            if (notifyCollection != null) {
                notifyCollection.CollectionChanged += (sender, args) => {
                    if (args.NewItems != null) {
                        foreach (var newItem in args.NewItems)
                            picker.Items.Add((newItem ?? "").ToString());
                    }

                    if (args.OldItems == null)
                        return;

                    foreach (var oldItem in args.OldItems)
                        picker.Items.Remove((oldItem ?? "").ToString());
                };
            }

            if (newValue == null)
                return;

            picker.Items.Clear();

            foreach (var item in newValue)
                picker.Items.Add((item ?? "").ToString());

            picker.RaisePropertyChanged("ItemsSource");
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedIndex < 0 || SelectedIndex >= ItemsSource.Count())
                SelectedItem = default(T);
            else
                SelectedItem = ItemsSource.ElementAt(SelectedIndex);
        }

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue) {
            var picker = (BindablePicker<T>) bindable;

            if (picker.ItemsSource == null)
                return;

            int index = 0;

            foreach (var item in picker.ItemsSource) {
                if (picker.SelectedItem == item) {
                    picker.SelectedIndex = index;

                    break;
                }

                index++;
            }

            picker.RaisePropertyChanged("SelectedItem");
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<BindablePicker<T>, IEnumerable<T>>(p => p.ItemsSource, null,
                propertyChanged: OnItemsSourcePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<BindablePicker<T>, object>(p => p.SelectedItem, null, BindingMode.TwoWay,
                propertyChanged: OnSelectedItemPropertyChanged);
    }
}