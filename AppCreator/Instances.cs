#region
// Analysis disable RedundantUsingDirective
using Acr.UserDialogs;
using AppCreator.Interfaces;
using Plugin.Battery;
using Plugin.Battery.Abstractions;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
// Analysis restore RedundantUsingDirective

#endregion

namespace AppCreator {
	public static class Instances {
		public static IUserDialogs Dialogs => UserDialogs.Instance;
		public static IScreenSize ScreenSize => DependencyService.Get<IScreenSize>();
		public static IGeolocator Geolocator => CrossGeolocator.Current;
		public static IDeviceInfo DeviceInfo => CrossDeviceInfo.Current;
		public static IConnectivity Connectivity => CrossConnectivity.Current;
		public static IBattery Battery => CrossBattery.Current;
	}
}