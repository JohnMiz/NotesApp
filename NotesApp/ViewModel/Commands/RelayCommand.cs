using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
	 public class RelayCommand : ICommand
	 {
		  private Action _Action;

		  public event EventHandler CanExecuteChanged;

		  public RelayCommand(Action action)
		  {
			   _Action = action;
		  }

		  public bool CanExecute(object parameter)
		  {
			   return true;
		  }

		  public void Execute(object parameter)
		  {
			   if(_Action != null)
			   {
					_Action();
			   }
		  }
	 }
}
