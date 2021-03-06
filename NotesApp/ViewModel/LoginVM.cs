﻿using NotesApp.Model;
using NotesApp.Services;
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
	 public class LoginVM : ViewModelBase
	 {
		  private User _User;

		  public User User
		  {
			   get { return _User; }
			   set { _User = value; }
		  }

		  public UserAuth _UserAuth { get; set; }

		  private bool _IsLoginMode;

		  public bool IsLoginMode
		  {
			   get { return _IsLoginMode; }
			   set { SetProperty(ref _IsLoginMode, value); }
		  }


		  private Window _Window { get; set; }

		  public ICommand LoginCommand { get; set; }
		  public ICommand RegisterCommand { get; set; }
		  public ICommand NoAccountCommand { get; set; }
		  public ICommand HaveAccountCommand { get; set; }

		  private ILoginService _LoginService;

		  public LoginVM(Window window, ILoginService loginService)
		  {
			   _Window = window;

			   LoginCommand = new RelayCommand(Login);
			   RegisterCommand = new RelayCommand(Register);
			   NoAccountCommand = new RelayCommand(NoAccount);
			   HaveAccountCommand = new RelayCommand(HaveAccount);

			   _LoginService = loginService;

			   User = new User();
			   _UserAuth = new UserAuth(User);
			   IsLoginMode = true;
		  }

		  #region Command Functions

		  private async void Login()
		  {
			   string message = string.Empty;
			   if (_UserAuth.VerifyCredentials())
			   {
					try
					{
						 User user = await _LoginService.GetUserFromDB(User.Username);
						 // TODO: Add loading that will notify the user that the server is processing the data

						 if (user != null)
						 {
							  if (_UserAuth.VerifyPassword(user.Password))
							  {
								   App.UserId = user.Id;

								   GoToNotesWindow();

								   _Window.Close();
							  }
							  else
							  {
								   message = "Username or password not found!";
							  }
						 }
						 else
						 {
							  message = "Username or password not found!";
						 }
					}
					catch(Exception ex)
					{
						 MessageBox.Show(ex.Message);
					}

			   }
			   else
			   {
					message = "Enter username and password please!";
			   }

			   if(!string.IsNullOrEmpty(message))
					MessageBox.Show(message);
		  }

		  private async void Register()
		  {
			   if(_UserAuth.VerifyRegisterInfo())
			   {
					try
					{
						 await App.MobileServiceClient.GetTable<User>().InsertAsync(User);
						 App.UserId = User.Id;
						 IsLoginMode = true;
						 
					}
					catch(Exception ex)
					{
						 MessageBox.Show(ex.Message);
					}
			   }
			   else
			   {
					MessageBox.Show("Fill all the fields.");
			   }


		  }

		  private void NoAccount()
		  {
			   IsLoginMode = false;
		  }

		  private void HaveAccount()
		  {
			   IsLoginMode = true;
		  }

		  #endregion

		  private void GoToNotesWindow()
		  {
			   NotesWindow notesWindow = new NotesWindow();
			   notesWindow.Show();
		  }
	 }
}
