#region
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

#endregion

namespace Test.WinPhone {
    public partial class MainPage : FormsApplicationPage {
        public MainPage() {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Forms.Init();
            LoadApplication(new Test.App());
        }
    }
}