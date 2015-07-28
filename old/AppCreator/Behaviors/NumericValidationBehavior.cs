#region
using System;
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    public class NumericValidationBehavior : BaseValidatorBehavior {
        protected override void OnEntryTextChanged(object sender, TextChangedEventArgs args) {
            double result;
            var isValid = Double.TryParse(args.NewTextValue, out result);

            ((Entry) sender).TextColor = isValid ? ValidColor : InvalidColor;
        }
    }
}