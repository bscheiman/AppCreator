#region
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    public class MaxLengthBehavior : BaseValidatorBehavior {
        public int Length { get; set; }

        protected override void OnEntryTextChanged(object sender, TextChangedEventArgs e) {
            var entry = (Entry) sender;
            IsValid = entry.Text.Length <= Length;

            entry.TextColor = IsValid ? ValidColor : InvalidColor;
        }
    }
}