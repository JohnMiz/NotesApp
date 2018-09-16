using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
	 public class RelayParameterizedCommand<T> : ICommand
	 {
		  private Action<T> m_action;

		  public RelayParameterizedCommand(Action<T> action)
		  {
			   this.m_action = action;
		  }

		  public event EventHandler CanExecuteChanged
		  {
			   add { CommandManager.RequerySuggested += value; }
			   remove { CommandManager.RequerySuggested -= value; }
		  }

		  public bool CanExecute(object parameter)
		  {
			   return (parameter != null);
		  }

		  public void Execute(object parameter)
		  {
			   if (m_action != null)
			   {
					m_action((T)parameter);
			   }
		  }
	 }
}
