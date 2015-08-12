#region
using Acr.UserDialogs;

#endregion

namespace AppCreator {
    public static class Instances {
		public static IUserDialogs Dialogs { 
			get {
				return UserDialogs.Instance;
			}
		}
    }
}