using System;
using System.Runtime.CompilerServices;
using AppCreator.Interfaces;
using System.Drawing;
using Xamarin.Forms;
using Rectangle = System.Drawing.Rectangle;
using Android.Content;
using Android.Views;

[assembly: Xamarin.Forms.Dependency(typeof(AppCreator.Droid.Implementations.ScreenSizeImpl))]
namespace AppCreator.Droid.Implementations {
	public class ScreenSizeImpl : IScreenSize {
		public Rectangle ScreenSize {
			get {
				var wm = (IWindowManager) Forms.Context.GetSystemService(Context.WindowService);
				var display = wm.DefaultDisplay;
				var point = new Android.Graphics.Point();
				display.GetSize(point);

				return new Rectangle(0,
				                     0,
				                     point.X,
				                     point.Y);
			}
		}
	}
}

