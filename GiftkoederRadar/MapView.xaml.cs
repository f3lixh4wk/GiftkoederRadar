using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace GiftkoederRadar
{
	/// <summary>
	/// Interaktionslogik für MapView.xaml
	/// </summary>
	public partial class MapView : Page
	{
		public MapView(bool showProgress)
		{
			InitializeComponent();
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			tboxSearch.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxSearch.GotFocus += new RoutedEventHandler(textbox_enter);
			
			showProgressDialog = showProgress;
			reports = mainWindow.GetReports();
			InitReportList();
		}

		private void btnClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			if (btn == btnBack)
			{
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.StartView);
				mainWindow.ChangeView(new StartView());
			}
			else if (btn == btnAdd)
			{
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.ReportView);
				mainWindow.ChangeView(new ReportView(View.MapView));
			}
			else if (btn == btnSearchInMap)
			{
				string location = tboxSearch.Text;
				GeoCoderStatusCode statusCode = setMapPositionByKeywords(location);
				if (statusCode != GeoCoderStatusCode.OK)
				{
					MessageBoxResult dialogResult = MessageBox.Show
					(
						"Die Suche nach: " + location + " konnte leider nicht angezeigt werden",
						"Ungültige Suche",
						MessageBoxButton.OK, MessageBoxImage.Error
					);
					return;
				}
				mapView.Zoom = 13;
			}
		}

		private void textbox_leave(object sender, EventArgs e)
		{
			if (tboxSearch.Text.Length == 0)
			{
				tboxSearch.Text = "PLZ, Ort";
				tboxSearch.Foreground = Brushes.LightGray;
			}
		}

		private void textbox_enter(object sender, EventArgs e)
		{
			if (tboxSearch.Text == "PLZ, Ort")
			{
				tboxSearch.Text = "";
				tboxSearch.Foreground = Brushes.Black;
			}
		}

		private void menuItemClick(object sender, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			if (menuItem == menuItemOpenURL)
			{
				System.Diagnostics.Process.Start(new ProcessStartInfo
				{
					FileName = "https://www.giftkoeder-radar.com/",
					UseShellExecute = true
				});
			}
			else if (menuItem == menuItemAbout)
			{
				AboutWindow aboutWindow = new AboutWindow();
				aboutWindow.Owner = (MainWindow)Application.Current.MainWindow;
				aboutWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				aboutWindow.ShowDialog();
			}
		}

		private void mapViewLoaded(object sender, RoutedEventArgs e)
		{
			initGMap();
			if (showProgressDialog)
			{
				ProgressDialogWithTimer progressDialogWithTimer = new ProgressDialogWithTimer(5000);
				progressDialogWithTimer.Owner = (MainWindow)Application.Current.MainWindow;
				progressDialogWithTimer.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				Opacity = 0.5;
				progressDialogWithTimer.ShowDialog();
				Opacity = 1;
			}
		}

		private void InitReportList()
		{
			List<ReportItem> items = new List<ReportItem>();
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			foreach (Report report in reports)
			{
				items.Add(new ReportItem() 
				{ 
					ItemId = report.ReportId,
					ItemBaitTitle = report.BaitTitle, 
					ItemDescription = report.Description, 
					SketchFilePath = report.SketchFilePath
				});
			}
			lboxReportList.ItemsSource = items;
		}

		private void initGMap()
		{
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			mapView.MapProvider = GMapProviders.OpenStreetMap; // Für GoogleMaps wird ein API Schlüssel benötigt
			mapView.MinZoom = 2;
			mapView.MaxZoom = 17;
			mapView.Zoom = 12;
			mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
			mapView.CanDragMap = true;
			mapView.DragButton = MouseButton.Left;
			mapView.ShowCenter = false;
			mapView.SetPositionByKeywords("Göttingen, Germany");
		}

		private List<Report> reports;
		bool showProgressDialog = false;

		private void lboxReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ReportItem reportItem = (ReportItem)lboxReportList.SelectedItem;
			string postCode = getElementById(reportItem.ItemId, "PostCode");
			string town = getElementById(reportItem.ItemId, "Town");
			string street = getElementById(reportItem.ItemId, "Street");

			GeoCoderStatusCode statusCode = setMapPositionByKeywords(postCode);
			if(statusCode != GeoCoderStatusCode.OK)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Die Giftköder-Meldung mit der Postleitzahl: " + postCode + " konnte leider nicht angezeigt werden",
					"Ungültige Postleitzahl",
					MessageBoxButton.OK, MessageBoxImage.Error
				);
				return;
			}
			mapView.Zoom = 15;
			PointLatLng position = mapView.Position;
			GMapMarker marker = new GMapMarker(position);
			marker.Shape = new Image
			{
				Width = 30,
				Height = 30,
				Source = new BitmapImage(new Uri("/icons8-warning-25.png", UriKind.Relative))
			};
			mapView.Markers.Add(marker);
		}

		private GeoCoderStatusCode setMapPositionByKeywords(string postCode)
		{
			return mapView.SetPositionByKeywords(postCode);
		}

		private string getElementById(int id, string elementName)
		{
			string elementValue = "";
			foreach(Report report in reports)
			{
				if (report.ReportId == id && nameof(report.PostCode) == elementName)
					elementValue = report.PostCode;
				else if (report.ReportId == id && nameof(report.Town) == elementName)
					elementValue = report.Town;
				else if (report.ReportId == id && nameof(report.Street) == elementName)
					elementValue = report.Street;
			}
			return elementValue;
		}
	}

	public class ReportItem
	{
		public int ItemId { get; set; }
		public string ItemBaitTitle { get; set; }
		public string ItemDescription { get; set; }
		public string SketchFilePath { get; set; }
	}
}
