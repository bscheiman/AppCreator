#region
using System;
using System.Linq.Expressions;
using AppCreator.Interfaces;
using AppCreator.ViewModels;
using Xamarin.Forms;

#endregion

namespace AppCreator.Pages {
    public abstract class BasePage<TModel> : ContentPage, IBasePage where TModel : BaseViewModel, new() {
        private TModel _backingModel;

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

            // iOS 7 Status bar
            Padding = new Thickness(0, 0, 0, 0);
        }

        protected void Bind<TV>(BindableProperty property, Expression<Func<TModel, TV>> func) {
            SetBinding(property, new Binding((func.Body as MemberExpression).Member.Name));
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if (BackingModel.Updated && !BackingModel.CallUpdateOnAppear)
                return;

            await BackingModel.Update();
            BackingModel.Updated = true;
        }

        protected void ResetPadding() {
            Padding = new Thickness(0, 0, 0, 0);
        }
    }
}