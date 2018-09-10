
using System.ComponentModel;

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
}
