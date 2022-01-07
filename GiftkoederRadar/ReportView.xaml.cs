using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GiftkoederRadar
{
	/// <summary>
	/// Interaktionslogik für ReportView.xaml
	/// </summary>
	public partial class ReportView : Page
	{
		public ReportView(View callingView)
		{
			InitializeComponent();

			SizeChanged += new SizeChangedEventHandler(pageWillResize);
			tboxPostCode.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxPostCode.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxTown.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxTown.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxStreet.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxStreet.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxBaitTitle.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxBaitTitle.LostFocus += new RoutedEventHandler(textbox_leave);

			//2.3.5.DataBinding: das DataContext-Attribut, die Binding-Syntax (Ganze Vorlesung 9.Data Binding)
			report = new Report();
			DataContext = report;
			calledFromView = callingView;
		}

		private void textbox_enter(object sender, EventArgs e)
		{
			TextBox tbox = (TextBox)sender;
			if (tbox.Text.Length == 0)
				return;

			string tboxText = tbox.Text;
			if (tboxText == "Postleitzahl"
				|| tboxText == "Ort"
				|| tboxText == "Optional - Straße"
				|| tboxText == "z.B. Hackfleischstück mit Nägeln")
			{
				tbox.Text = "";
				tbox.Foreground = Brushes.Black;
			}

		}
		private void textbox_leave(object sender, EventArgs e)
		{
			TextBox tbox = (TextBox)sender;
			if (tbox.Text.Length == 0)
			{
				tbox.Foreground = Brushes.LightGray;
				if (tbox == tboxPostCode)
					tboxPostCode.Text = "Postleitzahl";
				else if (tbox == tboxTown)
					tboxTown.Text = "Ort";
				else if (tbox == tboxStreet)
					tboxStreet.Text = "Optional - Straße";
				else if (tbox == tboxBaitTitle)
					tboxBaitTitle.Text = "z.B. Hackfleischstück mit Nägeln";
			}
		}

		private void btnClick(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			if (btn == btnBack)
			{
				if (unsavedChanges())
				{
					MessageBoxResult dialogResult = MessageBox.Show
					(
						"Möchtest Sie die Meldung verwerfen?", "Meldung erstellen Abbrechen",
						MessageBoxButton.YesNo, MessageBoxImage.Warning
					);

					// Der User möchte die Meldung verwerfen
					if (dialogResult == MessageBoxResult.Yes)
						closeDialog = true;

					// Der User möchte die Skizze nicht verwerfen
					else if (dialogResult == MessageBoxResult.No)
						return;
				}

				closeDialog = true;
				if (calledFromView == View.StartView && closeDialog)
				{
					MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
					mainWindow.SetActiveView(View.StartView);
					mainWindow.ChangeView(new StartView());
				}
				else if (calledFromView == View.MapView && closeDialog)
				{
					MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
					mainWindow.SetActiveView(View.MapView);
					mainWindow.ChangeView(new MapView());
				}
			}
			else if (btn == btnOpenSketch)
			{
				/*2.3.2.Modaler Dialog und eingegebene Werte aus modalem Dialog herausbekommen (siehe
				z.B. Aufgabe 7.1 oder Beispiel ModalerDialog aus 3.WPF Basics)*/

				BaitSketch baitSketchWindow = new BaitSketch();
				baitSketchWindow.Owner = (MainWindow)Application.Current.MainWindow;
				baitSketchWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

				if (baitSketchWindow.ShowDialog() == true)
				{
					// Canvas Inhalt speichern
					sketchFilePath = baitSketchWindow.SketchFileName;
					loadBitmapToCanvas(sketchFilePath);
					btnOpenSketch.IsEnabled = false;
					btnOpenSketch.Content = new Image
					{
						Source = new BitmapImage(new Uri("/icons8-sketch-disabled-24.png", UriKind.Relative))
					};
				}
			}
			else if (btn == btnDeleteSketch)
			{
				canvasSketch.Children.Clear();
				btnOpenSketch.IsEnabled = true;
				btnOpenSketch.Content = new Image
				{
					Source = new BitmapImage(new Uri("/icons8-sketch-24.png", UriKind.Relative))
				};
			}
			else if(btn == btnCreateReport)
			{
				report.Country = cboxCountry.SelectedItem.ToString();
				report.PostCode = tboxPostCode.Text;
				report.Town = tboxTown.Text;
				report.Street = tboxStreet.Text;
				report.BaitTitle = tboxBaitTitle.Text;
				report.Description = tboxDescription.Text;
				try
				{
					//validateReport();
				}
				catch(Exception ex)
				{
					MessageBoxResult dialogResult = MessageBox.Show
					(
						ex.Message, "Meldung unvollständig",
						MessageBoxButton.OK, MessageBoxImage.Warning
					);
				}
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				report.ReportId = mainWindow.GetNextFreeReportId();
				mainWindow.AddReport(report);
				mainWindow.SetActiveView(View.MapView);
				mainWindow.ChangeView(new MapView());
			}
		}

		private bool unsavedChanges()
		{
			bool tboxPostCodeInitialState = tboxPostCode.Foreground == Brushes.LightGray && tboxPostCode.Text == "Postleitzahl";
			bool tboxTownInitialState = tboxTown.Foreground == Brushes.LightGray && tboxTown.Text == "Ort";
			bool tboxStreetInitialState = tboxStreet.Foreground == Brushes.LightGray && tboxStreet.Text == "Optional - Straße";
			bool tboxBaitTitleInitialState = tboxBaitTitle.Foreground == Brushes.LightGray && tboxBaitTitle.Text == "z.B. Hackfleischstück mit Nägeln";

			bool unsavedChanges = tboxPostCode.Text.Length != 0 && !tboxPostCodeInitialState;
			unsavedChanges = unsavedChanges || (tboxTown.Text.Length != 0 && !tboxTownInitialState);
			unsavedChanges = unsavedChanges || (tboxStreet.Text.Length != 0 && !tboxStreetInitialState);
			unsavedChanges = unsavedChanges || (tboxBaitTitle.Text.Length != 0 && !tboxBaitTitleInitialState);
			unsavedChanges = unsavedChanges || tboxDescription.Text.Length != 0;
			return unsavedChanges;
		}

		private void pageWillResize(object sender, EventArgs e)
		{
			canvasSketch.Children.Clear();
			loadBitmapToCanvas(sketchFilePath);
		}

		private void loadBitmapToCanvas(string filename)
		{
			if (filename == null || filename.Length == 0)
				return;

			// Dekodiere die Skizze
			Stream imageStreamSource = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			BitmapSource bitmapSource = decoder.Frames[0];

			double height = canvasSketch.ActualHeight;
			double width = canvasSketch.ActualWidth;

			bitmapSource = resizeImage(bitmapSource, (int)width, (int)height);
			Image myImage = new Image();
			myImage.Source = bitmapSource;
			myImage.Stretch = Stretch.None;
			canvasSketch.Children.Add(myImage);
			report.Image = myImage;
		}

		private static BitmapFrame resizeImage(ImageSource source, int width, int height)
		{
			Rect rect = new Rect(0, 0, width, height);
			DrawingVisual drawingVisual = new DrawingVisual();
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
				drawingContext.DrawDrawing(new ImageDrawing(source, rect));

			RenderTargetBitmap resizedImage = new RenderTargetBitmap
			(
				 width, height,
				 96, 96,
				 PixelFormats.Default
			);
			resizedImage.Render(drawingVisual);
			return BitmapFrame.Create(resizedImage);
		}

		public Report report { get; set; }

		private View calledFromView;
		private string sketchFilePath; // Falls Image nicht klappt hiermit probieren und Datei von Pfad neu laden
		private bool closeDialog = false;
	}
}
