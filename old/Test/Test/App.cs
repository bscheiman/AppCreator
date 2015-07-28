#region
using Xamarin.Forms;

#endregion

namespace Test {
    public class App : Application {
        public App() {
            MainPage = new ContentPage {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            XAlign = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnStart() {
            // Handle when your app starts
        }
    }
}