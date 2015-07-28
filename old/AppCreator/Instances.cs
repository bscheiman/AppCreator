#region
using Acr.UserDialogs;
using Xamarin.Forms;

#endregion

namespace AppCreator {
    public static class Instances {
        public static IUserDialogs Dialogs = DependencyService.Get<IUserDialogs>();
    }
}