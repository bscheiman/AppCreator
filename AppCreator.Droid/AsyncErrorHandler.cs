#region
using System;
using AppCreator.Helpers;

#endregion

namespace AppCreator.UI {
    public static class AsyncErrorHandler {
        public static void HandleException(Exception exception) {
			Util.Log(exception);
        }
    }
}