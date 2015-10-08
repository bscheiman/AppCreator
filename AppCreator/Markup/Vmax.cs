using AppCreator.Helpers;

namespace AppCreator.Markup {
	public class Vmax : RelativeTo {
		public override double RelativeFunction(double units) {
			return RelativeSizeHelper.Vmax(units);
		}
	}
}
