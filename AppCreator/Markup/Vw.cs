using AppCreator.Helpers;

namespace AppCreator.Markup {
	public class Vw : RelativeTo {
		public override double RelativeFunction(double units) {
			return RelativeSizeHelper.Vw(units);
		}
	}
}
