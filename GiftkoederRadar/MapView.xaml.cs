using System;
using System.Collections;
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
	// Weitere Ideen nach Abgabe:
	// Versenden der Giftködermeldung als Email
	// Anbindung an die Internetseite https://www.giftkoeder-radar.com/

	/// <summary>
	/// Interaktionslogik für MapView.xaml
	/// </summary>
	public partial class MapView : Page
	{
		public MapView(bool showProgress)
		{
			InitializeComponent();
			tboxSearch.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxSearch.GotFocus += new RoutedEventHandler(textbox_enter);

			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			showProgressDialog = showProgress;
			reports = mainWindow.GetReports();
			initReportList();
			initBaitMarker();
		}

		private void btnClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			if (btn == btnBack)
			{
				currentSelectedItem = null;
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.StartView);
				mainWindow.ChangeView(new StartView());
			}
			else if (btn == btnAdd)
			{
				currentSelectedItem = null;
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.ReportView);
				mainWindow.ChangeView(new ReportView(View.MapView));
			}
			else if (btn == btnSearchInMap)
			{
				currentSelectedItem = null;
				string location = tboxSearch.Text;
				if (location.Length == 0 || location == initialSearch)
					return;

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
			else if (btn == btnRemove)
			{
				List<ReportItem> reportItems = (List<ReportItem>)lboxReportList.ItemsSource;
				ReportItem reportItem = currentSelectedItem;
				if (reportItem == null)
					return;

				PointLatLng position = getPositionFromItem(reportItem);
				GMapMarker marker = mapView.Markers.First(x => x.Position == position);
				mapView.Markers.Remove(marker);

				reports.RemoveAll(report => report.ReportId == reportItem.ItemId);
				reportItems.Remove(reportItem);
				lboxReportList.ItemsSource = reportItems;
				lboxReportList.Items.Refresh();

				mapView.SetPositionByKeywords(standardLocation);
				mapView.Zoom = 12;
				currentSelectedItem = null;
			}
			else if(btn == btnExpandMap)
			{
				currentSelectedItem = null;
				mapView.SetPositionByKeywords(standardLocation);
				mapView.Zoom = 12;
			}
		}

		private void textbox_leave(object sender, EventArgs e)
		{
			currentSelectedItem = null;
			if (tboxSearch.Text.Length == 0)
			{
				tboxSearch.Text = initialSearch;
				tboxSearch.Foreground = Brushes.LightGray;
			}
		}

		private void textbox_enter(object sender, EventArgs e)
		{
			currentSelectedItem = null;
			if (tboxSearch.Text == initialSearch)
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

		private void initReportList()
		{
			List<ReportItem> items = new List<ReportItem>();
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

		private void initBaitMarker()
		{
			List<ReportItem> reportItems = (List<ReportItem>)lboxReportList.ItemsSource;
			foreach (ReportItem reportItem in reportItems)
			{
				PointLatLng position = getPositionFromItem(reportItem);
				string tooltip = createMarkerToolTipFromItem(reportItem);

				GMapMarker marker = new GMapMarker(position);
				marker.Shape = new Image
				{
					Width = 30,
					Height = 30,
					ToolTip = tooltip,
					Source = new BitmapImage(new Uri("/icons8-warning-25.png", UriKind.Relative))
				};
				mapView.Markers.Add(marker);
			}
		}

		private void initGMap()
		{
			GMaps.Instance.Mode = AccessMode.ServerOnly;

			// Für GoogleMaps wird ein API Schlüssel benötigt, daher wird OpenStreetMaps verwendet
			mapView.MapProvider = GMapProviders.OpenStreetMap;
			mapView.MinZoom = 2;
			mapView.MaxZoom = 17;
			mapView.Zoom = 12;
			mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
			mapView.CanDragMap = true;
			mapView.DragButton = MouseButton.Left;
			mapView.ShowCenter = false;
			mapView.SetPositionByKeywords(standardLocation);
		}

		private void lboxReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ReportItem reportItem = (ReportItem)lboxReportList.SelectedItem;
			if (reportItem == null)
				return;

			string postCode = getElementById(reportItem.ItemId, "PostCode");
			string town = getElementById(reportItem.ItemId, "Town");
			string street = getElementById(reportItem.ItemId, "Street");

			GeoCoderStatusCode statusCode;
			if (street.Length != 0 && street != Report.InitialStreet)
			{
				string keywords = street + ", " + town;
				statusCode = setMapPositionByKeywords(keywords);
			}
			else { statusCode = setMapPositionByKeywords(postCode); }

			if (statusCode != GeoCoderStatusCode.OK)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Die Giftköder-Meldung konnte leider nicht angezeigt werden",
					"Ungültige Adresse",
					MessageBoxButton.OK, MessageBoxImage.Error
				);
				return;
			}
			mapView.Zoom = 15;
			currentSelectedItem = reportItem;
		}

		private void lboxReportList_LostFocus(object sender, RoutedEventArgs e)
		{
			lboxReportList.UnselectAll();
		}

		private GeoCoderStatusCode setMapPositionByKeywords(string keywords)
		{
			return mapView.SetPositionByKeywords(keywords);
		}

		private PointLatLng getPositionFromItem(ReportItem reportItem)
		{
			Report report = reports.First(x => x.ReportId == reportItem.ItemId);
			if (report.Street.Length != 0 && report.Street != Report.InitialStreet)
			{
				string keywords = report.Street + ", " + report.Town;
				return mapView.GetPositionByKeywords(keywords);
			}
			return mapView.GetPositionByKeywords(report.PostCode);
		}

		private string createMarkerToolTipFromItem(ReportItem reportItem)
		{
			string tooltip = "";
			Report report = reports.First(x => x.ReportId == reportItem.ItemId);
			tooltip = report.BaitTitle + "\n" +
				"Datum: " + report.CreatedDate.ToString("dd.MM.yyyy") + "\n" +
				"PLZ: " + report.PostCode + "\n" +
				"Land: " + report.Country;
			return tooltip;
		}

		private string getElementById(int id, string elementName)
		{
			string elementValue = "";
			foreach (Report report in reports)
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

		private List<Report> reports;
		private bool showProgressDialog = false;
		private string standardLocation = "Göttingen, Germany";
		private string initialSearch = "PLZ, Ort";

		// Nicht so schön, aber so kann ich die zuletzt selektierte Meldung
		// mit btnRemove entfernen und erreichen, dass beim LostFocusEvent der
		// lboxReportList auch das selektierte Item seinen Fokus verliert.
		private ReportItem currentSelectedItem;
	}

	public class ReportItem
	{
		public int ItemId { get; set; }
		public string ItemBaitTitle { get; set; }
		public string ItemDescription { get; set; }
		public string SketchFilePath { get; set; }
	}
}
