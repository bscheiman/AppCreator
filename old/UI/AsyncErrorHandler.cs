﻿#region
using System;
using System.Diagnostics;

#endregion

namespace AppCreator.UI {
    public static class AsyncErrorHandler {
        public static void HandleException(Exception exception) {
			Debug.WriteLine(exception);
        }
    }
}