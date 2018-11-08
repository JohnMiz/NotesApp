using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Model;
using NotesApp.ViewModel;

namespace NotesApp.Services
{
	 public class SqliteLoginService : ILoginService
	 {
		  private User _GetUserFromDB(string username)
		  {
			   using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(SqliteDatabaseHelper.dbFile))
			   {
					conn.CreateTable<User>();

					var dbUser = conn.Table<User>().Where(u => u.Username == username).FirstOrDefault();

					// TODO: Add loading that will notify the user that the server is processing the data

					if (dbUser != null)
					{
						 return dbUser;
					}

			   }

			   return null;
		  }

		  public async Task<User> GetUserFromDB(string username)
		  {
			   var user = await Task.Run(() => { return _GetUserFromDB(username); });
			   return user;
		  }

		  public void Login()
		  {
			   throw new NotImplementedException();
		  }
	 }
}
