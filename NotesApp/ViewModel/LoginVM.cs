using NotesApp.Model;
using NotesApp.View;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NotesApp.ViewModel
{
	 public class LoginVM
	 {
		  private User _User;

		  public User User
		  {
			   get { return _User; }
			   set { _User = value; }
		  }

		  private Window _Window { get; set; }

		  public ICommand LoginCommand { get; set; }

		  public LoginVM(Window window)
		  {
			   _Window = window;
			   LoginCommand = new RelayCommand(Login);
			   User = new User();
		  }

		  private bool ValidateUser(User user)
		  {
			   if(string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Username))
			   {
					return false;
			   }

			   // TODO: Check if the username already exists in the db

			   return true;
		  }

		  private void Login()
		  {
			   if(ValidateUser(User))
			   {
					using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
					{
						 conn.CreateTable<User>();

						 var user = conn.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();

						 if(user != null)
						 {
							  if(user.Password == User.Password)
							  {
								   GoToNotesWindow();

								    _Window.Close();
							  }
							  else
							  {
								   MessageBox.Show("Username or password not found!");
							  }
						 }
						 else
						 {
							  MessageBox.Show("Username or password not found!");
						 }
					}
			   }
			   else
			   {
					MessageBox.Show("Enter username and password please!");
			   }
		  }

		  private void GoToNotesWindow()
		  {
			   NotesWindow notesWindow = new NotesWindow();
			   notesWindow.Show();
		  }
	 }
}
