using Xamarin.Forms;
using System;
using AppCreator.Helpers;
using Xamarin.Forms.Xaml;

namespace AppCreator.Markup {
	[ContentProperty("Units")]
	public class Vw : IMarkupExtension {
		public string Units { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider) {
			if (Units == null)
				return null;
			
			return RelativeSizeHelper.Vw(double.Parse(Units));
		}
	}
}
