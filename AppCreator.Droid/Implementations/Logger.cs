using System;
using Xamarin.Forms;
using AppCreator.Interfaces;

[assembly: Dependency(typeof(AppCreator.Droid.Implementations.Logger))]
namespace AppCreator.Droid.Implementations {
	public class Logger : ILogger {
		public void Log(string str) {
			Console.WriteLine(str);
		}

		public void Log(string str, params object[] parms) {
			Console.WriteLine(string.Format(str, parms));
		}
	}
}

