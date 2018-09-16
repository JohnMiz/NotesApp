using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
	 public class Note : INotifyPropertyChanged
	 {

		  private int _Id;

		  [PrimaryKey, AutoIncrement]
		  public int Id
		  {
			   get { return _Id; }
			   set { _Id = value; OnPropertyChanged(nameof(Id)); }
		  }

		  private int _NotebookId;
		  [Indexed]
		  public int NotebookId
		  {
			   get { return _NotebookId; }
			   set { _NotebookId = value; OnPropertyChanged(nameof(NotebookId)); }
		  }

		  private string _Title;

		  public string Title
		  {
			   get { return _Title; }
			   set { _Title = value; OnPropertyChanged(nameof(Title)); }
		  }

		  private DateTime _CreatedTime;

		  public DateTime CreatedTime
		  {
			   get { return _CreatedTime; }
			   set { _CreatedTime = value; OnPropertyChanged(nameof(CreatedTime)); }
		  }

		  private DateTime _UpdatedTime;

		  public DateTime UpdatedTime
		  {
			   get { return _UpdatedTime; }
			   set { _UpdatedTime = value; OnPropertyChanged(nameof(UpdatedTime)); }
		  }

		  private string _FileLocation;

		  public string FileLocation
		  {
			   get { return _FileLocation; }
			   set { _FileLocation = value; OnPropertyChanged(nameof(FileLocation)); }
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
