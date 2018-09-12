using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace NotesApp.ViewModel.Converters
{
	 public class InverseBoolToVisConverter : MarkupExtension, IValueConverter
	 {
		  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		  {
			   bool IsVisible = (bool)value;

			   if (IsVisible)
					return Visibility.Collapsed;
			   else return Visibility.Visible;
		  }

		  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		  {
			   return false;
		  }

		  public override object ProvideValue(IServiceProvider serviceProvider)
		  {
			   return this;
		  }
	 }
}
