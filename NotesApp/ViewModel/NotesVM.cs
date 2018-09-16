using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel
{
	 public class NotesVM : ObservablePropertyNotifier
	 {
		  public ObservableCollection<Notebook> Notebooks { get; set; }

		  public ObservableCollection<Note> Notes { get; set; }

		  private Notebook _SelectedNotebook;

		  public Notebook SelectedNotebook
		  {
			   get { return _SelectedNotebook; }
			   set
			   {
					if (_SelectedNotebook == value)
						 return;

					_SelectedNotebook = value;

					OnPropertyChanged(nameof(SelectedNotebook));
			   }
		  }
		  
		  public ICommand NewNotebookCommand { get; set; }
		  public ICommand BeginEditCommand { get; set; }
		  public ICommand EndEditCommand { get; set; }

		  public ICommand NewNoteCommand { get; set; }


		  public NotesVM()
		  {
			   Notebooks = new ObservableCollection<Notebook>();

			   NewNotebookCommand = new RelayCommand(NewNotebook);
			   BeginEditCommand = new RelayParameterizedCommand<Notebook>(BeginEdit);
			   EndEditCommand = new RelayParameterizedCommand<Notebook>(EndEdit);

			   NewNoteCommand = new RelayParameterizedCommand<Notebook>(NewNote);

			   ReadNotebooks();

		  }

		  private void NewNote(Notebook notebook)
		  {

		  }

		  private void EndEdit(Notebook notebook)
		  {
			   if(notebook != null)
			   {
					notebook.IsEditing = false;
					DatabaseHelper.Update(notebook);
			   }
		  }

		  private void BeginEdit(Notebook notebook)
		  {
			   notebook.IsEditing = true;
		  }

		  private void NewNotebook()
		  {
			   DatabaseHelper.Insert(CreateNewNotebook());

			   Notebooks.Clear();

			   ReadNotebooks();
		  }

		  private void ReadNotebooks()
		  {
			   using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
			   {
					var notebooks = conn.Table<Notebook>().ToList();

					foreach(var notebook in notebooks)
					{
						 if (notebook.UserId == App.UserId)
						 {
							  Notebooks.Add(notebook);
						 }
					}
			   }
		  }

		  Notebook CreateNewNotebook()
		  {
			   Notebook notebook = new Notebook
			   {
					UserId = App.UserId,
					Name = "New Notebook"
			   };

			   return notebook;
		  }
	 }
}
