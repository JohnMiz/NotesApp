using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace NotesApp.ViewModel
{

	 public class NotesVM : ViewModelBase
	 {
		  private Notebook _SelectedNotebook;
		  private Note _SelectedNote;


		  public ObservableCollection<Notebook> Notebooks { get; set; }
		  public ObservableCollection<Note> Notes { get; set; }

		  public Notebook SelectedNotebook
		  {
			   get { return _SelectedNotebook; }
			   set
			   {
					if(SetProperty(ref _SelectedNotebook, value))
					{
						 ReadNotes();
					}
			   }
		  }

		  public Note SelectedNote
		  {
			   get { return _SelectedNote; }
			   set
			   {
					if (SetProperty(ref _SelectedNote, value))
					{
						 SelectedNoteChanged(this, new EventArgs());
					}
			   }
		  }

		  public ICommand NewNotebookCommand { get; private set; }
		  public ICommand NotebookBeginEditCommand { get; private set; }
		  public ICommand NotebookEndEditCommand { get; private set; }
		  public ICommand DeleteNotebookCommand { get; private set; }

		  public ICommand NewNoteCommand { get; private set; }
		  public ICommand NoteBeginEditCommand { get; private set; }
		  public ICommand NoteEndEditCommand { get; private set; }
		  

		  public event EventHandler SelectedNoteChanged;

		  public NotesVM()
		  {
			   Notebooks = new ObservableCollection<Notebook>();
			   Notes = new ObservableCollection<Note>();

			   NewNotebookCommand = new RelayCommand(NewNotebook);
			   NotebookBeginEditCommand = new RelayParameterizedCommand<Notebook>(NotebookBeginEdit);
			   NotebookEndEditCommand = new RelayParameterizedCommand<Notebook>(NotebookEndEdit);

			   NoteBeginEditCommand = new RelayParameterizedCommand<Note>(NoteBeginEdit);
			   NoteEndEditCommand = new RelayParameterizedCommand<Note>(NoteEndEdit);

			   DeleteNotebookCommand = new RelayParameterizedCommand<Notebook>(DeleteNotebook);

			   NewNoteCommand = new RelayParameterizedCommand<Notebook>(NewNote);

			   ReadNotebooks();
			   ReadNotes();
		  }

		  private async void NewNotebook()
		  {
			   await App.MobileServiceClient.GetTable<Notebook>().InsertAsync(CreateNewNotebook());

			   Notebooks.Clear();

			   ReadNotebooks();
		  }

		  private void NotebookBeginEdit(Notebook notebook)
		  {
			   notebook.IsEditing = true;
		  }

		  private async void NotebookEndEdit(Notebook notebook)
		  {
			   if (notebook != null)
			   {
					notebook.IsEditing = false;
					await App.MobileServiceClient.GetTable<Notebook>().UpdateAsync(notebook);
			   }
		  }

		  private async void DeleteNotebook(Notebook notebook)
		  {
			   // Delete each note from the datebase

			   foreach(var note in Notes)
			   {
					if(note.NotebookId == notebook.Id)
					{
						 try
						 {
							  await App.MobileServiceClient.GetTable<Note>().DeleteAsync(note);
						 }
						 catch (Exception ex)
						 {
							  Debug.WriteLine(ex.Message);
						 }
						 
					}
			   }

			   // Delete the notebook
			   try
			   {
					await App.MobileServiceClient.GetTable<Notebook>().DeleteAsync(notebook);
			   }
			   catch (Exception ex)
			   {
					Debug.WriteLine(ex.Message);
			   }

			   ReadNotebooks();
			   ReadNotes();
		  }

		  private async void NewNote(Notebook notebook)
		  {
			   if (notebook != null)
			   {
					Note newNote = new Note
					{
						 NotebookId = notebook.Id,
						 CreatedTime = DateTime.Now,
						 UpdatedTime = DateTime.Now,
						 Title = "New Note",

					};

					try
					{
						 await App.MobileServiceClient.GetTable<Note>().InsertAsync(newNote);
					}
					catch (Exception ex)
					{
						 Debug.WriteLine(ex.Message);
					}

					ReadNotes();
			   }
		  }

		  private void NoteBeginEdit(Note note)
		  {
			   if (note != null)
			   {
					note.IsEditing = true;
			   }
		  }

		  private async void NoteEndEdit(Note note)
		  {
			   if (note != null)
			   {
					note.IsEditing = false;
					try
					{
						 await App.MobileServiceClient.GetTable<Note>().UpdateAsync(note);
					}
					catch(Exception ex)
					{
						 Debug.WriteLine(ex.Message);
					}
			   }
		  }

		  private Notebook CreateNewNotebook()
		  {
			   Notebook notebook = new Notebook
			   {
					UserId = App.UserId,
					Name = "New Notebook"
			   };

			   return notebook;
		  }

		  private async void ReadNotes()
		  {
			   try
			   {
					var notes = (await App.MobileServiceClient.GetTable<Note>().ToListAsync()).Where(n => n.NotebookId == SelectedNotebook.Id);

					Notes.Clear();

					foreach(var note in notes)
					{
						 Notes.Add(note);
					}
			   }
			   catch (Exception ex)
			   {
					Debug.WriteLine(ex.Message);
			   }

		  }
		  
		  private async void ReadNotebooks()
		  {
			   // NOTE: Bad handling of try catch
			   try
			   {
					var notebooks = await App.MobileServiceClient.GetTable<Notebook>().Where(n => n.UserId == App.UserId).ToListAsync();

					Notebooks.Clear();

					foreach (var notebook in notebooks)
					{
						 Notebooks.Add(notebook);
					}
			   }
			   catch (Exception ex)
			   {
					Debug.WriteLine(ex.Message);
			   }
		  }

		  public async void UpdateSelectedNote()
		  {
			   await App.MobileServiceClient.GetTable<Note>().UpdateAsync(SelectedNote);
		  }
	 }
}
