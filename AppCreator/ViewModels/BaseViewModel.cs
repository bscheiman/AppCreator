#region
using System.Threading.Tasks;
using AppCreator.Custom;
using PropertyChanged;
using PropertyChanging;
using Xamarin.Forms;

#endregion

namespace AppCreator.ViewModels {
    [ImplementPropertyChanged, ImplementPropertyChanging, ToString]
    public class BaseViewModel {
        public string Icon { get; set; }
        public bool IsBusy { get; set; }
        public INavigation Navigation { get; set; }
        public ContentPage Page { get; set; }
        public string Title { get; set; }

        public virtual async Task Update() {
        }
    }

    [ImplementPropertyChanged, ImplementPropertyChanging, ToString]
    public class BaseViewModel<T> : BaseViewModel {
        public ObservableCollectionEx<T> Collection { get; set; }

        public BaseViewModel() {
            Collection = new ObservableCollectionEx<T>();
        }
    }
}