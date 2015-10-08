using AppCreator.Helpers;

namespace AppCreator.Markup {
	public class Vh : RelativeTo {
		public override double RelativeFunction(double units) {
			return RelativeSizeHelper.Vh(units);
		}
	}
}

