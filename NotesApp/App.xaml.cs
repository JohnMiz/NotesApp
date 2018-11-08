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
		  public static string UserId = "";
		  public static string AZURE_SERVER_URL = "FILL_YOUR_AZURE_SERVER_URL";
		  public static MobileServiceClient MobileServiceClient = new MobileServiceClient(AZURE_SERVER_URL);

		  protected override void OnStartup(StartupEventArgs e)
		  {
			   base.OnStartup(e);

			   while (string.IsNullOrEmpty(UserId)) 
			   {
					new LoginWindow().ShowDialog();
			   }
		  }
	 }
}
