using System;
using AppCreator.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCreator.Markup {
	[ContentProperty("Units")]
	public class Vmin : IMarkupExtension {
		public string Units { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) {
			if (Units == null)
				return null;

			return RelativeSizeHelper.Vmin(double.Parse(Units));
		}
	}
}
