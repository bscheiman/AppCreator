#region
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Pages {
    public class CustomNavPage : NavigationPage {
        public CustomNavPage(Page p) : base(p) {
            if (string.IsNullOrEmpty(Title))
                Title = "\u00A0"; // non breaking space, aka &nbsp;
        }
    }
}