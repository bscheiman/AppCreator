#region
// Analysis disable RedundantUsingDirective
using Acr.UserDialogs;
using AppCreator.Interfaces;
using Battery.Plugin.Abstractions;
using Connectivity.Plugin.Abstractions;
using DeviceInfo.Plugin.Abstractions;
using Geolocator.Plugin.Abstractions;
using Geolocator.Plugin;
using Battery.Plugin;
using Connectivity.Plugin;
using DeviceInfo.Plugin;
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