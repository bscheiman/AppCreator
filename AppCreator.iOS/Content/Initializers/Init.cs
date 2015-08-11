using System;
using AppCreator.UI.Controls;
using Acr.UserDialogs;

namespace AppCreator.iOS {
	public static class AppCreatorUI {
		public static void Init() {
			var spacer = typeof(Spacer);

			UserDialogs.Init();
		}
	}
}

