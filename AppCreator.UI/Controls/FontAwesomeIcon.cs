#region
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Controls {
    public class FontAwesomeIcon : Label {
        public const string Typeface = "FontAwesome";

        public FontAwesomeIcon(string fontAwesomeIcon = null) {
            FontFamily = string.Format("{0}{1}", Typeface, (Device.OS == TargetPlatform.Android ? ".ttf" : ""));
            Text = fontAwesomeIcon;
        }
    }
}