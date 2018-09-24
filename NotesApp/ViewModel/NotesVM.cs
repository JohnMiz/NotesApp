using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NotesApp.ViewModel
{
	 public class ContentLength : ObservablePropertyNotifier
	 {
		  private int _Length;

		  public int Length
		  {
			   get { return _Length; }
			   set
			   {

					if (_Length == value)
						 return;

					_Length = value;
					OnPropertyChanged(nameof(Length));
			   }
		  }

		  public override string ToString()
		  {
			   return $"Document length: {Length - 2} characters";
		  }
	 }

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

		  //private string _NoteContent;

		  //public string NoteContent
		  //{
			 //  get { return _NoteContent; }
			 //  set
			 //  {
				//	if (_NoteContent == value)
				//		 return;

				//	_NoteContent = value;
				//	ContentLength.Length = _NoteContent.Length;
				//	OnPropertyChanged(nameof(NoteContent));

			 //  }
		  //}

		  //private int _ContentLength;

		  //public int ContentLength
		  //{
			 //  get { return _ContentLength; }
			 //  set
			 //  {

				//	if (_ContentLength == value)
				//		 return;

				//	_ContentLength = value;
				//	OnPropertyChanged(nameof(ContentLength));
				//	Debug.WriteLine(ContentLength);
			 //  }

		  //}

		  private ContentLength _ContentLength = new ContentLength();

		  public ContentLength ContentLength
		  {
			   get { return _ContentLength; }
			   set { _ContentLength = value; }
		  }

		  private SpeechRecognitionEngine _Recognizer { get; set; }


		  public ICommand NewNotebookCommand { get; set; }
		  public ICommand NotebookBeginEditCommand { get; set; }
		  public ICommand NotebookEndEditCommand { get; set; }
		  public ICommand NoteBeginEditCommand { get; set; }
		  public ICommand NoteEndEditCommand { get; set; }

		  public ICommand NewNoteCommand { get; set; }
		  public ICommand SpeechCommand { get; set; }


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

			   SpeechCommand = new RelayParameterizedCommand<bool>(Speech);

			   ReadNotebooks();
			   ReadNotes();


			   var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
									 where r.Culture.Equals(Thread.CurrentThread.CurrentUICulture)
									 select r).FirstOrDefault();

			   // TODO: Replace that with Microsoft Speech API
			   _Recognizer = new SpeechRecognitionEngine(currentCulture);

			   GrammarBuilder builder = new GrammarBuilder();
			   builder.AppendDictation();
			   Grammar grammaer = new Grammar(builder);

			   _Recognizer.LoadGrammar(grammaer);
			   _Recognizer.SetInputToDefaultAudioDevice();
			   _Recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

		  }

		  private void Speech(bool IsChecked)
		  {
			   if (IsChecked)
			   {
					_Recognizer.RecognizeAsync(RecognizeMode.Multiple);
			   }
			   else
			   {
					_Recognizer.RecognizeAsyncStop();
			   }
		  }

		  private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		  {
			   //NoteContent += $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{e.Result.Text}";
		  }

		  private void NoteEndEdit(Note note)
		  {
			   if (note != null)
			   {
					note.IsEditing = false;
					DatabaseHelper.Update(note);
			   }
		  }

		  private void NoteBeginEdit(Note note)
		  {
			   if (note != null)
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
			   if (notebook != null)
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

					foreach (var notebook in notebooks)
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
