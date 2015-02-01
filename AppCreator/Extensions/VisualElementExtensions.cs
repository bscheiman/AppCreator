using Xamarin.Forms;

namespace AppCreator.Extensions {
    public static class VisualElementExtensions {
        public static TView BindTo<TView>(this TView visualElement, BindableProperty targetProperty, string modelProperty, string fmt = "")
            where TView : Element {
            visualElement.SetBinding(targetProperty,
                string.IsNullOrEmpty(fmt) ? new Binding(modelProperty) : new Binding(modelProperty, stringFormat: fmt));

            return visualElement;
        }
    }
}