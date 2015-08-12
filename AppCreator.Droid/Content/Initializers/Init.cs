using System;
using AppCreator.UI.Controls;
using Xamarin.Forms;
using Acr.UserDialogs;
using Android.App;

namespace AppCreator.Droid {
	public static class AppCreatorUI {
		public static void Init(Activity activity) {
#pragma warning disable 0219
			var spacer = typeof(Spacer);

			UserDialogs.Init(activity);
#pragma warning restore 0219
		}
	}
}

