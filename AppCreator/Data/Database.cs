using System;
using Xamarin.Forms;
using AppCreator.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SQLite.Net.Async;
using SQLite.Net;

namespace AppCreator.Data {
	public static class Database {
		static string DbFilename = DependencyService.Get<IPathHelper>().GetLibraryFullPath("config.db3");
		static SQLiteAsyncConnection DbContext { get; set; }
		static internal bool IsSetup { get; set; }

		static Database() {

		}

		public static void Setup(Func<SQLiteConnectionWithLock> sqlFunc) {
			DbContext = new SQLiteAsyncConnection(sqlFunc);
			IsSetup = true;
		}

		public static void Setup<TObject>() where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			DbContext.CreateTableAsync<TObject>();
		}

		public static async Task<List<TObject>> Get<TObject>() where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			return await DbContext.Table<TObject>().ToListAsync();
		}

		public static async Task<List<TObject>> Get<TObject>(Expression<Func<TObject, bool>> pred) where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			return await DbContext.Table<TObject>().Where(pred).ToListAsync();
		}

		public static async Task<TObject> Get<TObject>(int id) where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			return await DbContext.FindAsync<TObject>(x => x.Id == id);
		}

		public static async Task<int> Save<TObject>(TObject item) where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			if (item.Id != 0) {
				await DbContext.UpdateAsync(item);

				return item.Id;
			}

			return await DbContext.InsertAsync(item);
		}

		public static async Task<int> Delete<TObject>(TObject obj) where TObject : BaseDbObject, new() {
			if (!IsSetup)
				throw new ArgumentException("Database hasn't been configured yet");

			return await DbContext.DeleteAsync(obj.Id);
		}
	}
}

