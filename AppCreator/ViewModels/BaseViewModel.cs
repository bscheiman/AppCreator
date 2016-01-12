#region
using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PropertyChanged;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;
using Plugin.Connectivity.Abstractions;

#endregion

namespace AppCreator.ViewModels {
    [ImplementPropertyChanged]
    public class BaseViewModel {
        public bool CallUpdateOnAppear { get; set; }
        public string Icon { get; set; }
        public bool IsBusy { get; set; }
        public INavigation Navigation { get; set; }
        public ContentPage Page { get; set; }
        public string Title { get; set; }
        public bool Updated { get; set; }

		protected IDictionary<string, object> Properties { 
			get { return Application.Current.Properties; } 
		}

		public BaseViewModel() : this(true) {
		}

		public BaseViewModel(bool updateOnAppear) {
			CallUpdateOnAppear = updateOnAppear;
		}

		public virtual void OnAppearing() {
		}

		public virtual void OnDisappearing() {
		}

        protected Task Alert(string title = "App", string message = "", string okButton = "Ok") {
            return Alert(new AlertConfig {
                Title = title,
                Message = message,
                OkText = okButton
            });
        }

        protected Task Alert(AlertConfig cfg) {
            return Instances.Dialogs.AlertAsync(cfg);
        }

        protected Task<bool> Confirm(string title = "App", string message = "", string okButton = "Yes", string noButton = "No") {
            return Confirm(new ConfirmConfig {
                Title = title,
                Message = message,
                OkText = okButton,
                CancelText = noButton
            });
        }

        protected Task<bool> Confirm(ConfirmConfig cfg) {
            return Instances.Dialogs.ConfirmAsync(cfg);
        }

        protected Task<PromptResult> Prompt(string title = "App", string message = "", string okButton = "Ok") {
            return Prompt(new PromptConfig {
                Title = title,
                Message = message,
                OkText = okButton
            });
        }

        protected Task<PromptResult> Prompt(PromptConfig cfg) {
            return Instances.Dialogs.PromptAsync(cfg);
        }

		protected void Toast(string message, ToastEvent evt = ToastEvent.Success, int timeoutSeconds = 3, Action act = null) {
			Instances.Dialogs.Toast(new ToastConfig(evt, message) {
				Duration = TimeSpan.FromSeconds((double) timeoutSeconds),
				Action = act
			});
        }

		public virtual async Task Update(bool isRefresh = false) {
            await Task.Run(() => { });
        }

		public ICommand Refresh {
			get {
				return new Command(async s => {
					IsBusy = true;
					await Update(true);
					IsBusy = false;
				});
			}
		}

		protected async Task SavePropertiesAsync() {
			await Application.Current.SavePropertiesAsync();
		}

		protected bool IsConnected => Instances.Connectivity.IsConnected;
		protected IEnumerable<ConnectionType> ConnectionTypes => Instances.Connectivity.ConnectionTypes;
    }

    [ImplementPropertyChanged]
    public class BaseViewModel<T> : BaseViewModel {
		public List<T> Collection { get; set; }

        public BaseViewModel() {
			Collection = new List<T>();
        }
    }
}