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
					ReadNotes();

					OnPropertyChanged(nameof(SelectedNotebook));
			   }
		  }

		  private Note _SelectedNote;

		  public Note SelectedNote
		  {
			   get { return _SelectedNote; }
			   set
			   {
					if (_SelectedNote == value)
						 return;

					_SelectedNote = value;

					OnPropertyChanged(nameof(SelectedNote));
			   }
		  }

		  private string _NoteContent;

		  public string NoteContent
		  {
			   get { return _NoteContent; }
			   set {
					if (_NoteContent == value)
						 return;

					_NoteContent = value;
					OnPropertyChanged(nameof(NoteContent));

			   }
		  }


		  public ICommand NewNotebookCommand { get; set; }
		  public ICommand NotebookBeginEditCommand { get; set; }
		  public ICommand NotebookEndEditCommand { get; set; }
		  public ICommand NoteBeginEditCommand { get; set; }
		  public ICommand NoteEndEditCommand { get; set; }

		  public ICommand NewNoteCommand { get; set; }
		  public ICommand TextChangedCommand { get; set; }


		  public NotesVM()
		  {
			   Notebooks = new ObservableCollection<Notebook>();
			   Notes = new ObservableCollection<Note>();

			   NewNotebookCommand = new RelayCommand(NewNotebook);
			   NotebookBeginEditCommand = new RelayParameterizedCommand<Notebook>(NotebookBeginEdit);
			   NotebookEndEditCommand = new RelayParameterizedCommand<Notebook>(NotebookEndEdit);
			   NoteBeginEditCommand = new RelayParameterizedCommand<Note>(NoteBeginEdit);
			   NoteEndEditCommand = new RelayParameterizedCommand<Note>(NoteEndEdit);

			   NewNoteCommand = new RelayParameterizedCommand<Notebook>(NewNote);

			   TextChangedCommand = new RelayCommand(RichTextChanged);

			   ReadNotebooks();
			   ReadNotes();

		  }

		  private void RichTextChanged()
		  {
			   
		  }

		  private void NoteEndEdit(Note note)
		  {
			   if(note != null)
			   {
					note.IsEditing = false;
					DatabaseHelper.Update(note);
			   }
		  }

		  private void NoteBeginEdit(Note note)
		  {
			   if(note != null)
			   {
					note.IsEditing = true;
			   }
					
		  }

		  private void ReadNotes()
		  {
			   using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
			   {
					conn.CreateTable<Note>();

					if (SelectedNotebook != null)
					{
						 Notes.Clear();

						 var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

						 foreach (var note in notes)
						 {
							  Notes.Add(note);
						 }
					}
			   }
		  }

		  private void NewNote(Notebook notebook)
		  {
			   Note newNote = new Note
			   {
					NotebookId = notebook.Id,
					CreatedTime = DateTime.Now,
					UpdatedTime = DateTime.Now,
					Title = "New Note",

			   };

			   DatabaseHelper.Insert(newNote);

			   ReadNotes();
		  }

		  private void NotebookEndEdit(Notebook notebook)
		  {
			   if(notebook != null)
			   {
					notebook.IsEditing = false;
					DatabaseHelper.Update(notebook);
			   }
		  }

		  private void NotebookBeginEdit(Notebook notebook)
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
