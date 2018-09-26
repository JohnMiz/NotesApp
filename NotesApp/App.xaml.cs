using Microsoft.WindowsAzure.MobileServices;
using NotesApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NotesApp
{
	 /// <summary>
	 /// Interaction logic for App.xaml
	 /// </summary>
	 public partial class App : Application
	 {

		  public static int UserId = -1;
		  //public static MobileServiceClient MobileServiceClient = new MobileServiceClient("YOUR AZURE SERVER HERE");

		  protected override void OnStartup(StartupEventArgs e)
		  {
			   base.OnStartup(e);

			   while (UserId == -1)
			   {

					new LoginWindow().ShowDialog();
					Debug.WriteLine("!23");
			   }
		  }
	 }
}
