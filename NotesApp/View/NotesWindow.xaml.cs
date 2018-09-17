﻿using NotesApp.Model;
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
		  }

		  private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
		  {
			   var TextRange = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);

			   statusTextBlock.Text = $"Document length: {TextRange.Text.Length - 2} characters";
		  }
	 }
}
