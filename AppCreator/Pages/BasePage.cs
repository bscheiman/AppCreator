#region
using System;
using System.Linq.Expressions;
using AppCreator.Interfaces;
using AppCreator.ViewModels;
using Xamarin.Forms;
using PropertyChanged;
using System.Collections.Generic;

#endregion

namespace AppCreator.Pages {
	internal static class ShownCache {
		internal static HashSet<string> Shown = new HashSet<string>();

		internal static void SetShown<TModel>(Type page) where TModel : BaseViewModel, new() {
			Shown.Add(KeyFor<TModel>(page));
		}

		internal static string KeyFor<TModel>(Type page) {
			return string.Format("{0}-{1}", page.GetType(), typeof(TModel));
		}

		internal static bool HasBeenShown<TModel>(Type page) {
			return Shown.Contains(KeyFor<TModel>(page));
		}
	}

	[ImplementPropertyChanged]
    public abstract class BasePage<TModel> : ContentPage, IBasePage where TModel : BaseViewModel, new() {
        private TModel _backingModel;
		public bool AddStatusBarPadding { get; set; }

        public TModel BackingModel {
            get {
                return _backingModel ?? (_backingModel = new TModel {
                    Navigation = Navigation,
                    Page = this
                });
            }
        }

        protected BasePage() {
            BindingContext = BackingModel;

            Bind(IsBusyProperty, m => m.IsBusy);
            Bind(TitleProperty, m => m.Title);
            Bind(IconProperty, m => m.Icon);
        }

		protected void OnAddStatusBarPaddingChanged() {
			Padding = AddStatusBarPadding ? new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0) : new Thickness(0, 0, 0, 0);

		}

        protected void Bind<TV>(BindableProperty property, Expression<Func<TModel, TV>> func) {
            SetBinding(property, new Binding((func.Body as MemberExpression).Member.Name));
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

			if (!ShownCache.HasBeenShown<TModel>(GetType()))
				await BackingModel.Setup();

			ShownCache.SetShown<TModel>(GetType());
			BackingModel.OnAppearing();

            if (BackingModel.Updated && !BackingModel.CallUpdateOnAppear)
                return;

			BackingModel.IsBusy = true;
			await BackingModel.Update(false);
			BackingModel.IsBusy = false;
            BackingModel.Updated = true;
        }

        protected void ResetPadding() {
            Padding = new Thickness(0, 0, 0, 0);
        }

		protected override void OnDisappearing() {
			base.OnDisappearing();
			BackingModel.OnDisappearing();
		}
    }
}