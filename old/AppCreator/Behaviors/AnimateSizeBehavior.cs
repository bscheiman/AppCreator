#region
using System;
using PropertyChanged;
using Xamarin.Forms;

#endregion

namespace AppCreator.Behaviors {
    [ImplementPropertyChanged]
    public class AnimateSizeBehavior : Behavior<View> {
        public string EasingFunctionName { get; set; }
        public double Scale { get; set; }

        public AnimateSizeBehavior() {
            EasingFunctionName = "SinIn";
            Scale = 1.02;
        }

        private static Easing GetEasing(string easingName) {
            switch (easingName) {
                case "BounceIn":
                    return Easing.BounceIn;

                case "BounceOut":
                    return Easing.BounceOut;

                case "CubicInOut":
                    return Easing.CubicInOut;

                case "CubicOut":
                    return Easing.CubicOut;

                case "Linear":
                    return Easing.Linear;

                case "SinIn":
                    return Easing.SinIn;

                case "SinInOut":
                    return Easing.SinInOut;

                case "SinOut":
                    return Easing.SinOut;

                case "SpringIn":
                    return Easing.SpringIn;

                case "SpringOut":
                    return Easing.SpringOut;

                default:
                    throw new ArgumentException(string.Format("{0} is not valid", easingName));
            }
        }

        protected override void OnAttachedTo(View bindable) {
            bindable.Focused += OnFocused;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable) {
            bindable.Focused -= OnFocused;

            base.OnDetachingFrom(bindable);
        }

        private async void OnFocused(object sender, FocusEventArgs e) {
            var v = sender as View;
            var f = GetEasing(EasingFunctionName);

            await v.ScaleTo(Scale, 250, f);
            await v.ScaleTo(1.00, 250, f);
        }
    }
}