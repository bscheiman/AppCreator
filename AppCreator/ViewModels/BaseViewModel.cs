using System.Threading.Tasks;
using AppCreator.Custom;
using PropertyChanged;
using PropertyChanging;
using Xamarin.Forms;
using System.ServiceModel.Channels;
using Acr.XamForms.UserDialogs;

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

		protected Task Alert(string title = "App", string message = "", string okButton = "Ok") {
			return Alert(new AlertConfig {
				Title = title,
				Message = message,
				OkText = okButton
			});
		}

		protected Task<PromptResult> Prompt(string title = "App", string message = "", string okButton = "Ok") {
			return Prompt(new PromptConfig {
				Title = title,
				Message = message,
				OkText = okButton
			});
		}

		protected Task<bool> Confirm(string title = "App", string message = "", string okButton = "Yes", string noButton = "No") {
			return Confirm(new ConfirmConfig {
				Title = title,
				Message = message,
				OkText = okButton,
				CancelText = noButton
			});
		}

		protected Task Alert(AlertConfig cfg) {
			return Instances.Dialogs.AlertAsync(cfg);
		}

		protected Task<PromptResult> Prompt(PromptConfig cfg) {
			return Instances.Dialogs.PromptAsync(cfg);
		}

		protected Task<bool> Confirm(ConfirmConfig cfg) {
			return Instances.Dialogs.ConfirmAsync(cfg);
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

