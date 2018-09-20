using NotesApp.Model;
using NotesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesApp.View
{
	 /// <summary>
	 /// Interaction logic for NotesWindow.xaml
	 /// </summary>
	 public partial class NotesWindow : Window
	 {
		  public NotesWindow()
		  {
			   InitializeComponent();

			   var fonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			   fontFamilyComboBox.ItemsSource = fonts;

			   var fontSizes = new List<double>{ 10, 12, 14, 18, 24, 30, 36, 48, 60, 72, 96 };
			   fontSizeComboBox.ItemsSource = fontSizes;
		  }

		  private void boldButton_Click(object sender, RoutedEventArgs e)
		  {
			   bool IsChecked = (sender as ToggleButton).IsChecked ?? false;

			   if(IsChecked)
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
			   }
			   else
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
			   }
		  }

		  private void italicButton_Click(object sender, RoutedEventArgs e)
		  {
			   bool IsChecked = (sender as ToggleButton).IsChecked ?? false;

			   if(IsChecked)
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
			   }
			   else
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
			   }
		  }

		  private void underlineButton_Click(object sender, RoutedEventArgs e)
		  {
			   bool IsChecked = (sender as ToggleButton).IsChecked ?? false;
			   if(IsChecked)
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
			   }
			   else
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
			   }
		  }

		  private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
		  {
			   var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
			   boldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue && selectedWeight.Equals(FontWeights.Bold);

			   var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
			   italicButton.IsChecked = selectedStyle != DependencyProperty.UnsetValue && selectedStyle.Equals(FontStyles.Italic);

			   var selectedTextDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			   underlineButton.IsChecked = selectedTextDecoration != DependencyProperty.UnsetValue && selectedTextDecoration.Equals(TextDecorations.Underline);

			   fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
			   fontSizeComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
		  }

		  private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		  {
			   if (fontFamilyComboBox.SelectedItem != null)
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
			   }
		  }

		  private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		  {
			   if(fontSizeComboBox.SelectedItem != null)
			   {
					contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.SelectedItem);
			   }
		  }



		  //private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
		  //{
		  //  var TextRange = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

		  //  statusTextBlock.Text = $"Document length: {TextRange.Text.Length - 2} characters";
		  //}
	 }
}
