using System;
using Xamarin.Forms;

namespace AppCreator.Helpers {
	public static class RelativeSizeHelper {
		public static ResizeUnit Vh(double units) {
			return (Instances.ScreenSize.ScreenSize.Height / 100.00) * units;			
		}

		public static ResizeUnit Vw(double units) {
			return (Instances.ScreenSize.ScreenSize.Width / 100.00) * units;			
		}

		public static ResizeUnit Vmin(double units) {
			return (Math.Min(Instances.ScreenSize.ScreenSize.Width, Instances.ScreenSize.ScreenSize.Height) / 100.00) * units;			
		}

		public static ResizeUnit Vmax(double units) {
			return (Math.Max(Instances.ScreenSize.ScreenSize.Width, Instances.ScreenSize.ScreenSize.Height) / 100.00) * units;			
		}

		public class ResizeUnit {
			public double Value { get; set; }

			public ResizeUnit(double value) {
				Value = value;
			}

			public static implicit operator ResizeUnit(double value) {
				return new ResizeUnit(value);
			}

			public static implicit operator double(ResizeUnit rUnit) {
				return rUnit.Value;
			}

			public static implicit operator float(ResizeUnit rUnit) {
				return (float) rUnit.Value;
			}

			public static implicit operator int(ResizeUnit rUnit) {
				return (int) rUnit.Value;
			}

			public static implicit operator GridLength(ResizeUnit rUnit) {
				return new GridLength(rUnit.Value);
			}

			public static implicit operator Thickness(ResizeUnit rUnit) {
				return new Thickness(rUnit.Value);
			}
		}
	}
}

