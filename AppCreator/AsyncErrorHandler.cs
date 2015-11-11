#region
using System;
using AppCreator.Helpers;

#endregion

namespace AppCreator {
    public static class AsyncErrorHandler {
        public static void HandleException(Exception exception) {
			Util.Log(exception);
        }
    }
}