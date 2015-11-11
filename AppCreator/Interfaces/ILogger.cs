using System;

namespace AppCreator.Interfaces {
	public interface ILogger {
		void Log(string str);
		void Log(string str, params object[] parms);
	}
}

