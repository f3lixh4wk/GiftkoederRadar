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

			//2.3. WPF-Events (Click, MouseEnter…)
			//1.2. Event 
			tboxPostCode.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxPostCode.TextChanged += new TextChangedEventHandler(textbox_textChanged);
			tboxTown.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxTown.TextChanged += new TextChangedEventHandler(textbox_textChanged);
			tboxStreet.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxBaitTitle.GotFocus += new RoutedEventHandler(textbox_enter);
			tboxBaitTitle.TextChanged += new TextChangedEventHandler(textbox_textChanged);
			tboxDescription.TextChanged += new TextChangedEventHandler(textbox_textChanged);

			//2.3.5.DataBinding: das DataContext-Attribut, die Binding-Syntax (Ganze Vorlesung 9.Data Binding)
			report = new Report();
			DataContext = report;
			calledFromView = callingView;
		}

		public void ClearSketch()
		{
			canvasSketch.Children.Clear();
		}

		private void textbox_textChanged(object sender, EventArgs e)
		{
			// Den Pflichtfeldindikator * überprüfen
			TextBox tbox = (TextBox)sender;
			if (tbox == tboxPostCode)
			{
				StackPanel stackPanel = (StackPanel)lblPostCode.Content;
				updateMandatoryIndicator(stackPanel, tbox.Text, Report.InitialPostCode);
			}
			else if(tbox == tboxTown)
			{
				StackPanel stackPanel = (StackPanel)lblTown.Content;
				updateMandatoryIndicator(stackPanel, tbox.Text, Report.InitialTown);
			}
			else if (tbox == tboxBaitTitle)
			{
				StackPanel stackPanel = (StackPanel)lblBaitTitle.Content;
				updateMandatoryIndicator(stackPanel, tbox.Text, Report.InitialBaitTitle);
			}
			else if (tbox == tboxDescription)
			{
				StackPanel stackPanel = (StackPanel)lblDescription.Content;
				updateMandatoryIndicator(stackPanel, tbox.Text, "");
			}
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

					// Der User möchte die Meldung nicht verwerfen
					if (dialogResult == MessageBoxResult.No)
						return;
				}
				if (calledFromView == View.StartView)
				{
					MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
					mainWindow.SetActiveView(View.StartView);
					mainWindow.ChangeView(new StartView());
				}
				else if (calledFromView == View.MapView)
				{
					MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
					mainWindow.SetActiveView(View.MapView);
					bool showProgressDialog = false;
					mainWindow.ChangeView(new MapView(showProgressDialog));
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
					loadBitmapToCanvas();
					btnOpenSketch.IsEnabled = false;
					btnOpenSketch.Content = new Image
					{
						Source = new BitmapImage(new Uri("/icons8-sketch-disabled-24.png", UriKind.Relative))
					};
				}
			}
			else if (btn == btnDeleteSketch)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Wollen Sie die Skizze wirklich löschen?", "Sizze löschen",
					MessageBoxButton.YesNo, MessageBoxImage.Warning
				);
				// Der User möchte die Skizze nicht löschen
				if (dialogResult == MessageBoxResult.No)
					return;

				canvasSketch.Children.Clear();
				if (File.Exists(sketchFilePath))
					File.Delete(sketchFilePath);

				btnOpenSketch.IsEnabled = true;
				btnOpenSketch.Content = new Image
				{
					Source = new BitmapImage(new Uri("/icons8-sketch-24.png", UriKind.Relative))
				};
			}
			else if(btn == btnCreateReport)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Sind alle Angaben korrekt?", "Meldung erstellen",
					MessageBoxButton.YesNo, MessageBoxImage.Warning
				);

				// Der User möchte die Meldung erstellen
				if (dialogResult == MessageBoxResult.Yes)
				{
					report.Country = cboxCountry.SelectedItem.ToString();
					report.PostCode = tboxPostCode.Text;
					report.Town = tboxTown.Text;
					report.Street = tboxStreet.Text;
					report.BaitTitle = tboxBaitTitle.Text;
					report.Description = tboxDescription.Text;
					report.SketchFilePath = File.Exists(sketchFilePath) ? sketchFilePath : "";
					
					//1.6. Ausnahmen (try, catch, throw)
					try
					{
						validateReport();
					}
					catch (Exception ex)
					{
						dialogResult = MessageBox.Show
						(
							ex.Message, "Meldung unvollständig",
							MessageBoxButton.OK, MessageBoxImage.Warning
						);
						return;
					}
					canvasSketch.Children.Clear();
					MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
					report.ReportId = mainWindow.GetNextFreeReportId();
					mainWindow.AddReport(report);
					mainWindow.SetActiveView(View.MapView);
					bool showProgressDialog = true;
					mainWindow.ChangeView(new MapView(showProgressDialog));
				}
			}
		}

		// Wirft Exception sobald ein Pflichtfeld nicht ausgefüllt wurde
		private void validateReport()
		{
			if (report.Country.Length == 0)
				throw new Exception("Es muss ein Land ausgewählt werden!");
			if (report.PostCode.Length == 0 || report.PostCode == Report.InitialPostCode)
				throw new Exception("Es muss eine Postleitzahl angegeben werden!");
			if(report.Town.Length == 0 || report.Town == Report.InitialTown)
				throw new Exception("Es muss eine Stadt angegeben werden!");
			if(report.BaitTitle.Length == 0 || report.BaitTitle == Report.InitialBaitTitle)
				throw new Exception("Es muss eine Giftköder-Beschreibung angegeben werden!");
			if(report.Description.Length == 0)
				throw new Exception("Es muss angegeben werden, was passiert ist!");
		}

		// 1.3. Methoden
		void updateMandatoryIndicator(StackPanel stackPanel, string text, string initialText)
		{
			foreach(object child in stackPanel.Children)
			{
				TextBlock textBlock = (TextBlock)child;
				if(textBlock.Foreground == Brushes.Red)
				{
					textBlock.Text = (text.Length == 0 || text == initialText) ? "*" : "";
				}
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
			loadBitmapToCanvas();
		}

		private void loadBitmapToCanvas()
		{
			if (sketchFilePath == null || sketchFilePath.Length == 0)
				return;

			// Dekodiere die Skizze
			Stream imageStreamSource = new FileStream(sketchFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			BitmapSource bitmapSource = decoder.Frames[0];

			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.AddStreamSource(imageStreamSource);

			double height = canvasSketch.ActualHeight;
			double width = canvasSketch.ActualWidth;

			bitmapSource = resizeImage(bitmapSource, (int)width, (int)height);
			Image image = new Image();
			image.Source = bitmapSource;
			image.Stretch = Stretch.None;
			canvasSketch.Children.Add(image);
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

		private Report report;
		private View calledFromView;
		private string sketchFilePath;
	}
}
