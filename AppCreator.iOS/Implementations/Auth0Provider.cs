//using Auth0.SDK;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using UIKit;
//using AppCreator.iOS.Implementations;
//using AppCreator.Interfaces;
//using Newtonsoft.Json.Linq;
//using Auth0User = AppCreator.Interfaces.Auth0User;
//
//[assembly: Dependency(typeof(Auth0Provider))]
//namespace AppCreator.iOS.Implementations {
//	public class Auth0Provider : IAuth0 {
//		public Auth0Client Auth { get; set; }
//		public static string Domain { get; set; }
//		public static string ClientId { get; set; }
//
//		public async Task<Auth0User> LoginAsync(string db, string username, string password) {
//			Auth = new Auth0Client(Domain, ClientId);
//			var res = await Auth.LoginAsync(db, username, password);
//
//			return new Auth0User {
//				Profile = res.Profile,
//				Auth0AccessToken = res.Auth0AccessToken,
//				IdToken = res.IdToken,
//				RefreshToken = res.RefreshToken
//			};
//		}
//
//		public async Task<Auth0User> LoginAsync(Page page, string provider) {
//			Auth = new Auth0Client(Domain, ClientId);
//			var window = UIApplication.SharedApplication.KeyWindow;
//			var vc = window.RootViewController;
//
//			while (vc.PresentedViewController != null)
//				vc = vc.PresentedViewController;
//
//			var res = await Auth.LoginAsync(vc, provider);
//
//			return new Auth0User {
//				Profile = res.Profile,
//				Auth0AccessToken = res.Auth0AccessToken,
//				IdToken = res.IdToken,
//				RefreshToken = res.RefreshToken
//			};
//		}	
//	
//		public Task<Auth0User> LoginWithExternalProviderAsync(Page page, string provider, string token) {
//			throw new System.NotImplementedException();
//		}
//
//		public Task<JObject> RefreshTokenAsync(string token) {
//			throw new System.NotImplementedException();
//		}
//	}
//}
//
