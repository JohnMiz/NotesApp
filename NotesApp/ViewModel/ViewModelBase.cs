
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotesApp.ViewModel
{
	 public class ViewModelBase : INotifyPropertyChanged
	 {
		  public event PropertyChangedEventHandler PropertyChanged;

		  public bool SetProperty<T>(ref T dest, T value, [CallerMemberName]string propertyName = null)
		  {
			   if(object.Equals(dest, value))
			   {
					return false;
			   }

			   dest = value;
			   OnPropertyChanged(propertyName);
			   return true;
		  }

		  private void OnPropertyChanged(string propertyName)
		  {
			   if(PropertyChanged != null)
			   {
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			   }
		  }
	 }
}
