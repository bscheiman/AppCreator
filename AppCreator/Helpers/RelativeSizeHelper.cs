using System;

namespace AppCreator.Helpers {
	public static class RelativeSizeHelper {
		public static double Vh(double units) {
			return (Instances.ScreenSize.ScreenSize.Height / 100.00) * units;			
		}

		public static double Vw(double units) {
			return (Instances.ScreenSize.ScreenSize.Width / 100.00) * units;			
		}

		public static double Vmin(double units) {
			return (Math.Min(Instances.ScreenSize.ScreenSize.Width, Instances.ScreenSize.ScreenSize.Height) / 100.00) * units;			
		}

		public static double Vmax(double units) {
			return (Math.Max(Instances.ScreenSize.ScreenSize.Width, Instances.ScreenSize.ScreenSize.Height) / 100.00) * units;			
		}
	}
}

