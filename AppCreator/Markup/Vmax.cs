using System;
using AppCreator.Helpers;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace AppCreator.Markup {
	[ContentProperty("Units")]
	public class Vmax : IMarkupExtension {
		public string Units { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) {
			if (Units == null)
				return null;

			return RelativeSizeHelper.Vmax(double.Parse(Units));
		}
	}
}
