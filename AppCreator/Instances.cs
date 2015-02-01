using Acr.XamForms.UserDialogs;
using Xamarin.Forms;

namespace AppCreator {
    public static class Instances {
        public static IUserDialogService Dialogs = DependencyService.Get<IUserDialogService>();
    }
}