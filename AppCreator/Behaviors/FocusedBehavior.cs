#region
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    [ImplementPropertyChanged]
    public class FocusedBehavior : Behavior<View> {
        public Color BlurredColor { get; set; }
        public Color FocusedColor { get; set; }

        public FocusedBehavior() {
            FocusedColor = Color.Green;
            BlurredColor = Color.Default;
        }

        protected override void OnAttachedTo(View view) {
            view.Focused += OnFocused;
            view.Unfocused += OnUnfocused;

            base.OnAttachedTo(view);
        }

        protected override void OnDetachingFrom(View view) {
            view.Focused -= OnFocused;
            view.Unfocused -= OnUnfocused;

            base.OnDetachingFrom(view);
        }

        private void OnFocused(object sender, FocusEventArgs e) {
            (sender as View).BackgroundColor = FocusedColor;
        }

        private void OnUnfocused(object sender, FocusEventArgs e) {
            (sender as View).BackgroundColor = BlurredColor;
        }
    }
}