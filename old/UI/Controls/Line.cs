#region
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Controls {
    [ImplementPropertyChanged]
    public class Line : BoxView {
        public Line() {
            Color = Color.Black;
            HeightRequest = 1;
        }
    }
}