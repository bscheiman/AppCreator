#region
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.UI.Controls {
    [ImplementPropertyChanged]
    public class EntryIcon : StackLayout {
        internal Entry Entry { get; set; }
        internal Image Image { get; set; }
        public string Placeholder { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public EntryIcon() {
            Entry = new Entry {
                BindingContext = this
            };

            Image = new Image {
                BindingContext = this
            };

            Entry.SetBinding(Entry.TextProperty, "Text", BindingMode.TwoWay);
            Entry.SetBinding(Entry.TextColorProperty, "TextColor", BindingMode.TwoWay);
            Entry.SetBinding(Entry.PlaceholderProperty, "Placeholder", BindingMode.TwoWay);

            Image.SetBinding(Image.SourceProperty, "Icon", BindingMode.TwoWay);
        }
    }
}