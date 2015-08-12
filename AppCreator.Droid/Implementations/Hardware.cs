using Xamarin.Forms;
using Android.OS;

[assembly: Dependency(typeof(AppCreator.Droid.Hardware))]
namespace AppCreator.Droid {
	public class Hardware : IHardware {
		public bool IsSimulator {
			get {
				return Build.Fingerprint.Contains("generic");
			}
		}
	}
}

