using System;
using Xamarin.Forms;
using AppCreator.Interfaces;
using Rectangle = System.Drawing.Rectangle;
using UIKit;

[assembly: Dependency(typeof(AppCreator.iOS.Implementations.ScreenSizeImpl))]
namespace AppCreator.iOS.Implementations {
	public class ScreenSizeImpl : IScreenSize {
		public Rectangle ScreenSize {
			get {
				return new Rectangle(0,
				                                    0,
				                                    (int) UIScreen.MainScreen.Bounds.Width,
				                                    (int) UIScreen.MainScreen.Bounds.Height);
			}
		}
	}
}

