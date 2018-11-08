using NotesApp.Model;
using NotesApp.ViewModel;
using System;
using System.Collections;
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
		  private NotesVM _NotesVM;
		  private SpeechRecognitionEngine _Recognizer { get; set; }

		  public NotesWindow()
		  {
			   InitializeComponent();

			   _NotesVM = new NotesVM();
			   this.DataContext = _NotesVM;

			   _NotesVM.SelectedNoteChanged += _NotesVM_SelectedNoteChanged;

			   var fonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			   fontFamilyComboBox.ItemsSource = fonts;

			   var fontSizes = new List<double>{ 10, 12, 14, 18, 24, 30, 36, 48, 60, 72, 96 };
			   fontSizeComboBox.ItemsSource = fontSizes;

			   // Speech recognition

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

		  private void speechButton_Click(object sender, RoutedEventArgs e)
		  {
			   var IsChecked = (sender as ToggleButton).IsChecked ?? false;
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
			   contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(e.Result.Text)));
		  }

		  private void _NotesVM_SelectedNoteChanged(object sender, EventArgs e)
		  {
			   contentRichTextBox.Document.Blocks.Clear();
			   if (!string.IsNullOrEmpty(_NotesVM.SelectedNote.FileLocation))
			   {
					using (FileStream fileStream = new FileStream(_NotesVM.SelectedNote.FileLocation, FileMode.Open))
					{
						 var textRange = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

						 textRange.Load(fileStream, DataFormats.Rtf);
					}
			   }
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

		  private void saveFile_Click(object sender, RoutedEventArgs e)
		  {
			   var notesVM = DataContext as NotesVM;
			   if (notesVM.SelectedNote != null)
			   {

					string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{notesVM.SelectedNote.Id}.rtf");


					using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
					{
						 var TextRange = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
						 TextRange.Save(fileStream, DataFormats.Rtf);
					}

					notesVM.SelectedNote.FileLocation = rtfFile;
					notesVM.SelectedNote.UpdatedTime = DateTime.Now;

					notesVM.UpdateSelectedNote();

					MessageBox.Show("The note was successfully saved!");
			   }
			   else
			   {
					MessageBox.Show("Choose note to save to...");
			   }

		  }

		  private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
		  {
			   var TextRange = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

			   statusTextBlock.Text = $"Document length: {TextRange.Text.Length - 2} characters";
		  }


	 }
}
