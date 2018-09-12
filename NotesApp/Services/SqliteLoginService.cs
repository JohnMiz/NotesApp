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
		  public User GetUserFromDB(string username)
		  {
			   using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
			   {
					conn.CreateTable<User>();

					var dbUser = conn.Table<User>().Where(u => u.Username == username).FirstOrDefault();

					// TODO: Add loading that will notify the user that the server is processing the data

					if (dbUser != null)
					{
						 if (username == dbUser.Username)
							  return dbUser;
					}

			   }

			   return null;
		  }

		  public void Login()
		  {
			   throw new NotImplementedException();
		  }
	 }
}
