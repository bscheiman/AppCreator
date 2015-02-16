#region
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Controls {
    [ImplementPropertyChanged]
    public class Spacer : Frame {
        public int Space { get; set; }

        public Spacer() {
            Space = 10;
            HasShadow = false;
            BackgroundColor = Color.Transparent;
        }

        public void OnSpaceChanged() {
            Padding = new Thickness(0, 0, 0, Space);
        }
    }
}