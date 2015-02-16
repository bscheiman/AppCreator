#region
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    [ImplementPropertyChanged]
    public abstract class BaseValidatorBehavior : Behavior<Entry> {
        public Color InvalidColor { get; set; }
        public bool IsValid { get; set; }
        public Color ValidColor { get; set; }

        protected BaseValidatorBehavior() {
            ValidColor = Color.Default;
            InvalidColor = Color.Red;
        }

        protected override void OnAttachedTo(Entry entry) {
            entry.TextChanged += OnEntryTextChanged;

            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry) {
            entry.TextChanged -= OnEntryTextChanged;

            base.OnDetachingFrom(entry);
        }

        protected abstract void OnEntryTextChanged(object sender, TextChangedEventArgs e);
    }
}