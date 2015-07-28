#region
using System;
using AppCreator.ViewModels;
using Xamarin.Forms;

#endregion

namespace AppCreator.Custom {
    public class LoadingWrapper : IDisposable {
        public BaseViewModel Model { get; set; }

        public LoadingWrapper(BaseViewModel viewModel, string message = "Loading...") {
            Device.BeginInvokeOnMainThread(() => {
                (Model = viewModel).IsBusy = true;
                Instances.Dialogs.ShowLoading(message);
            });
        }

        public void Dispose() {
            Device.BeginInvokeOnMainThread(() => {
                Model.IsBusy = false;
                Instances.Dialogs.HideLoading();
            });
        }
    }
}