using System.Threading.Tasks;

namespace AppCreator.Interfaces {
	public interface INativeLogin {
		Task<string> GetNativeLogin(NativeProvider provider);
	}
}
