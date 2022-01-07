using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		public void AddReport(Report report)
		{
			reports.Add(report);
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
					"Nicht gespeicherte Änderungen gehen verloren", "Meldung erstellen Abbrechen",
					MessageBoxButton.YesNo, MessageBoxImage.Warning
				);

				// Der User möchte die Meldung verwerfen
				if (dialogResult == MessageBoxResult.Yes)
					e.Cancel = false;

				// Der User möchte die Skizze nicht verwerfen
				else if (dialogResult == MessageBoxResult.No)
					e.Cancel = true;
			}
		}

		private List<Report> reports;
		private View activeView;
	}
}
