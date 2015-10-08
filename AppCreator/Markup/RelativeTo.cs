using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace AppCreator.Markup {
	[ContentProperty("Units")]
	public abstract class RelativeTo : IMarkupExtension {
		public string Units { get; set; }
		Action<double, double> RelativeFunc { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) {
			if (Units == null)
				return null;

			return RelativeFunction(double.Parse(Units));
		}

		public abstract double RelativeFunction(double units);
	}
}

