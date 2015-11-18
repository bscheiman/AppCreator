using System;
using Xamarin.Forms;
using AppCreator.Interfaces;
using Foundation;

[assembly: Dependency(typeof(AppCreator.iOS.Implementations.Logger))]
namespace AppCreator.iOS.Implementations {
	[Preserve]
	public class Logger : ILogger {
		public void Log(string str) {
			Console.WriteLine(str);
		}

		public void Log(string str, params object[] parms) {
			Console.WriteLine(string.Format(str, parms));
		}
	}
}

