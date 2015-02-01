#region
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppCreator.Interfaces;
using AppCreator.ViewModels;

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

            PostInit();
        }

        protected string _<TV>(Expression<Func<TModel, TV>> func) {
            return (func.Body as MemberExpression).Member.Name;
        }

        protected void Bind<TV>(BindableProperty property, Expression<Func<TModel, TV>> func) {
            SetBinding(property, new Binding((func.Body as MemberExpression).Member.Name));
        }

        protected void Bind<TV>(BindableProperty property, Expression<Func<TV>> func) {
            SetBinding(property, new Binding((func.Body as MemberExpression).Member.Name));
        }

        protected TView Bind<TView, TV>(TView element, BindableProperty property, Expression<Func<TModel, TV>> func) where TView : Element {
            element.SetBinding(property, new Binding((func.Body as MemberExpression).Member.Name));

            return element;
        }

        protected Frame GetSpacing(int space) {
            return new Frame {
                Padding = new Thickness(0, 0, 0, space),
                BackgroundColor = Color.Transparent
            };
        }

        //protected abstract void ConfigureUI();
        protected virtual void HookEvents() {
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            // iOS 7 Status bar
            Padding = new Thickness(0, Device.OnPlatform(HasNavigationBar() ? 0 : 20, 0, 0), 0, 0);
        }

        protected virtual void PostUpdate() {
        }

        protected void ResetPadding() {
            Padding = new Thickness(0, 0, 0, 0);
        }

        protected ScrollView ScrollWrap(View view, int padding = 0, ScrollOrientation orientation = ScrollOrientation.Vertical) {
            return new ScrollView {
                Content = view,
                Padding = padding,
                Orientation = orientation
            };
        }

        private bool HasNavigationBar() {
            var navBar = Parent as NavigationPage;

            return navBar != null;
        }

        private async Task PostInit() {
            //ConfigureUI();
            HookEvents();

            await BackingModel.Update();

            PostUpdate();
        }
    }
}