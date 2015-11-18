#define DEBUG
using AppCreator.Interfaces;
using Xamarin.Forms;
using System.Diagnostics;

namespace AppCreator.Helpers {
	public static class Util {
		public static ILogger MainLogger { get; set; }

		static Util() {
			MainLogger = DependencyService.Get<ILogger>();
		}

		public static void Log(string str) {
			if (MainLogger != null)
				MainLogger.Log(str);
			else
				Debug.WriteLine(str);
		}

		public static void Log(object obj) {
			Log(obj.ToString());
		}

		public static void Log(string str, params object[] parms) {
			Log(string.Format(str, parms));
		}
	}
}

