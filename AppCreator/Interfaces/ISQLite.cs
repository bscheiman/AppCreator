using SQLite.Net;

namespace AppCreator.Interfaces {
	public interface ISQLite {
		SQLiteConnection GetConnection();
	}
}

