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
	 public class NotesVM
	 {
		  public ICommand NewNotebookCommand { get; set; }

		  public NotesVM()
		  {
			   NewNotebookCommand = new RelayCommand(NewNotebook);
		  }

		  private void NewNotebook()
		  {
			   throw new NotImplementedException();
		  }
	 }
}
