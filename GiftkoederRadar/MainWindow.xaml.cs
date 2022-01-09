using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
	public enum View
	{
		StartView = 0,
		MapView,
		ReportView
	}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			// TODO window minimum size
			InitializeComponent();
			reports = new List<Report>();
			Application.Current.MainWindow = this;
			Loaded += OnMainWindowLoaded;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
		}

		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{
			ChangeView(new StartView());
		}

		public void ChangeView(Page view)
		{
			MainFrame.NavigationService.Navigate(view);
		}

		public void SetActiveView(View view)
		{
			activeView = view;
		}

		// Füge ein stream hinzu, der auf eine sketch*.png verweist 
		public void AddStreamSource(Stream stream)
		{
			imageStreamSources.Add(stream);
		}

		public void AddReport(Report report)
		{
			reports.Add(report);
		}
		

		public List<Report> GetReports()
		{
			return reports;
		}

		public int GetNextFreeReportId()
		{
			return reports.Count;
		}

		private void MainWindowClosing(object sender, CancelEventArgs e)
		{
			if(activeView == View.ReportView)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Möchtest Sie das Programm wirklich beenden?\n" +
					"Nicht gespeicherte Änderungen gehen verloren!", "Meldung erstellen Abbrechen",
					MessageBoxButton.YesNo, MessageBoxImage.Warning
				);

				// Der User möchte die Meldung verwerfen
				if (dialogResult == MessageBoxResult.Yes)
				{
					closeStreamSources();
					RemoveAllSketchFiles();
					e.Cancel = false;
				}

				else if (dialogResult == MessageBoxResult.No)
					e.Cancel = true;
			}
			else if(activeView == View.MapView)
			{
				closeStreamSources();
				RemoveAllSketchFiles();
				e.Cancel = false;
			}
		}

		private void RemoveAllSketchFiles()
		{
			string sketchDirPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			int nextFreeFileIndex = 0;
			string sketchFilePath = sketchDirPath + "\\sketch" + nextFreeFileIndex.ToString() + pngSuffix;
			while (File.Exists(sketchFilePath))
			{
				//1.6. Ausnahmen (try, catch, throw)
				try
				{
					File.Delete(sketchFilePath);
				}
				catch (IOException)
				{
					//Datei ist momentan in Benutzung
				}

				++nextFreeFileIndex;
				sketchFilePath = sketchDirPath + "\\sketch" + nextFreeFileIndex.ToString() + pngSuffix;
			}
		}

		private void closeStreamSources()
		{
			foreach(Stream stream in imageStreamSources)
			{
				stream.Close();
			}
		}

		private List<Stream> imageStreamSources = new List<Stream>();
		private List<Report> reports;
		private View activeView;
		private string pngSuffix = ".png";
	}
}
