using Xamarin.Forms;
using Android.OS;
using AppCreator.Interfaces;

[assembly: Dependency(typeof(AppCreator.Droid.Implementations.Hardware))]
namespace AppCreator.Droid.Implementations {
	public class Hardware : IHardware {
		public bool IsSimulator {
			get {
				return Build.Fingerprint.Contains("generic");
			}
		}
	}
}

