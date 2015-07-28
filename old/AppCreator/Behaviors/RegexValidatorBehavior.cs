#region
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    public class RegexValidatorBehavior : BaseValidatorBehavior {
        public string RegEx { get; set; }

        protected override void OnEntryTextChanged(object sender, TextChangedEventArgs e) {
            IsValid = Regex.IsMatch(e.NewTextValue, RegEx, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            ((Entry) sender).TextColor = IsValid ? ValidColor : InvalidColor;
        }
    }
}