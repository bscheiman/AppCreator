using Xamarin.Forms;

[assembly: Dependency(typeof(AppCreator.iOS.Hardware))]
namespace AppCreator.iOS {
	public class Hardware : IHardware {
		public bool IsSimulator {
			get {
				throw new System.NotImplementedException();
			}
		}
	}
}

