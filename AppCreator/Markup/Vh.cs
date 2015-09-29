using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using AppCreator.Helpers;

namespace AppCreator.Markup {
	[ContentProperty("Units")]
	public class Vh : IMarkupExtension {
		public string Units { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) {
			if (Units == null)
				return null;

			return RelativeSizeHelper.Vh(double.Parse(Units));
		}
	}
}

