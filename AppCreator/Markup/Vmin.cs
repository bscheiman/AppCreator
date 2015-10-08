using AppCreator.Helpers;

namespace AppCreator.Markup {
	public class Vmin : RelativeTo {
		public override double RelativeFunction(double units) {
			return RelativeSizeHelper.Vmin(units);
		}
	}
}
