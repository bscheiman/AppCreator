using Xamarin.Forms;
using AppCreator.Interfaces;

[assembly: Dependency(typeof(AppCreator.iOS.Implementations.Hardware))]
namespace AppCreator.iOS.Implementations {
	public class Hardware : IHardware {
		public bool IsSimulator {
			get {
				throw new System.NotImplementedException();
			}
		}
	}
}

