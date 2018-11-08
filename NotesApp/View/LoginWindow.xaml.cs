using NotesApp.Services;
using NotesApp.ViewModel;
using System.Windows;

namespace NotesApp.View
{
	 /// <summary>
	 /// Interaction logic for LoginWindow.xaml
	 /// </summary>
	 public partial class LoginWindow : Window
	 {
		  public LoginWindow()
		  {
			   InitializeComponent();
			   DataContext = new LoginVM(this, new AzureLoginService()); // TODO: Inject SqliteLoginService
		  }

	 }
}
