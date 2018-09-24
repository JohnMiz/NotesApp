
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotesApp.ViewModel
{
	 public class ObservablePropertyNotifier : INotifyPropertyChanged
	 {
		  public event PropertyChangedEventHandler PropertyChanged;

		  public void OnPropertyChanged(string propertyName)
		  {
			   if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		  }
	 }

	 public class ViewModelBase
	 {
		  public ObservablePropertyNotifier notifier { get; set; }

		  public ViewModelBase()
		  {
			   notifier = new ObservablePropertyNotifier();
		  }
	 }

	 public class ObservableBase : INotifyPropertyChanged
	 {
		  public event PropertyChangedEventHandler PropertyChanged;

		  protected void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
		  {
			   if (Equals(storage, value))
			   {
					return;
			   }

			   storage = value;
			   OnPropertyChanged(propertyName);
		  }

		  protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	 }


}
