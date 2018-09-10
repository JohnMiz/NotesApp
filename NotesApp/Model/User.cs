using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
	 public class User : INotifyPropertyChanged
	 {
		  
		  private string _Id;

		  [PrimaryKey, AutoIncrement]
		  public string Id
		  {
			   get { return _Id; }
			   set { _Id = value; OnPropertyChanged(nameof(Id)); }
		  }

		  private string _Name;

		  [MaxLength (50)]
		  public string Name
		  {
			   get { return _Name; }
			   set { _Name = value; OnPropertyChanged(nameof(Name)); }
		  }

		  private string _Lastname;
		  [MaxLength(50)]
		  public string Lastname
		  {
			   get { return _Lastname; }
			   set { _Lastname = value; OnPropertyChanged(nameof(Lastname)); }
		  }

		  private string _Username;

		  public string Username
		  {
			   get { return _Username; }
			   set { _Username = value; OnPropertyChanged(nameof(Username)); }
		  }

		  private string _Email;

		  public string Email
		  {
			   get { return _Email; }
			   set { _Email = value; OnPropertyChanged(nameof(Email)); }
		  }

		  private string _Password;

		  public string Password
		  {
			   get { return _Password; }
			   set { _Password = value; OnPropertyChanged(nameof(Password)); }
		  }

		  public event PropertyChangedEventHandler PropertyChanged;

		  private void OnPropertyChanged(string propertyName)
		  {
			   if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		  }

	 }
}
