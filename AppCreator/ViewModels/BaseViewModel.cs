using System.Threading.Tasks;
using AppCreator.Custom;
using PropertyChanged;
using PropertyChanging;
using Xamarin.Forms;

namespace AppCreator.ViewModels {
	[ImplementPropertyChanged, ImplementPropertyChanging, ToString]
	public class BaseViewModel {
		public bool IsBusy { get; set; }
		public string Title { get; set; }
		public string Icon { get; set; }
		public INavigation Navigation { get; set; }
		public ContentPage Page { get; set; }

		public async virtual Task Update() {
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

