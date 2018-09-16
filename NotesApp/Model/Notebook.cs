using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
	 public class Notebook : INotifyPropertyChanged
	 {
		  private int _Id;

		  [PrimaryKey, AutoIncrement]
		  public int Id
		  {
			   get { return _Id; }
			   set { _Id = value; OnPropertyChanged(nameof(Id)); }
		  }

		  private int _UserId;

		  [Indexed]
		  public int UserId
		  {
			   get { return _UserId; }
			   set { _UserId = value; OnPropertyChanged(nameof(UserId)); }
		  }

		  private string _Name;

		  public string Name
		  {
			   get { return _Name; }
			   set { _Name = value; OnPropertyChanged(nameof(Name)); }

		  }

		  private bool _IsEditing = false;

		  public bool IsEditing
		  {
			   get { return _IsEditing; }
			   set { if (_IsEditing == value) return; _IsEditing = value; OnPropertyChanged(nameof(IsEditing)); }
		  }

		  public event PropertyChangedEventHandler PropertyChanged;
		  private void OnPropertyChanged(string propertyName)
		  {
			   if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		  }

	 }
}
