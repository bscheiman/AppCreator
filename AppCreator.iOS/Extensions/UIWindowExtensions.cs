using UIKit;

namespace AppCreator.iOS {
	public static class UIWindowExtensions {
		public static UIViewController GetTopmostViewController(this UIWindow window) {
			var vc = window.RootViewController;

			while (vc.PresentedViewController != null)
				vc = vc.PresentedViewController;

			return vc;
		}
	}
}

