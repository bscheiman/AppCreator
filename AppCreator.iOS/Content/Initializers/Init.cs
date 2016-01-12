using System;
using AppCreator.UI.Controls;
using Acr.UserDialogs;
using FFImageLoading.Forms.Touch;
using AppCreator.iOS.Implementations;

namespace AppCreator.iOS {
	public static class AppCreatorUI {
		public static void Init() {
#pragma warning disable 0219
			var spacer = typeof(Spacer);
			var userDialogs = typeof(UserDialogs);
			var dialog = typeof(Logger);
			CachedImageRenderer.Init();

#pragma warning restore 0219
		}
	}
}

