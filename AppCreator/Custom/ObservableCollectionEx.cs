#region
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

#endregion

namespace AppCreator.Custom {
    public class ObservableCollectionEx<T> : ObservableCollection<T> {
        public ObservableCollectionEx() {
        }

        public ObservableCollectionEx(IEnumerable<T> list) : this() {
            ClearAndAddRange(list);
        }

        public void AddRange(IEnumerable<T> list) {
            foreach (var item in list)
                Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void ClearAndAddRange(IEnumerable<T> list) {
            Items.Clear();

            AddRange(list);
        }
    }
}