using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace AppCreator.Interfaces {
	public interface IAuth0 {
		Task<Auth0User> LoginAsync(Page page, string provider);
		Task<Auth0User> LoginWithExternalProviderAsync(Page page, string provider, string token);
		Task<JObject> RefreshTokenAsync(string token);
	}

	public enum NativeProvider {
		Facebook,
		Twitter,
		Google
	}

	public class Auth0User {
		public string Auth0AccessToken { get; set; }
		public string IdToken { get; set; }
		public JObject Profile { get; set; }
		public string RefreshToken { get; set; }
	}
}

