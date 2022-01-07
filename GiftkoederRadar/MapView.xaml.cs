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

namespace GiftkoederRadar
{
	/// <summary>
	/// Interaktionslogik für MapView.xaml
	/// </summary>
	public partial class MapView : Page
	{
		public MapView(bool showProgressDialog)
		{
			InitializeComponent();
			tboxSearch.LostFocus += new RoutedEventHandler(textbox_leave);
			tboxSearch.GotFocus += new RoutedEventHandler(textbox_enter);
			if(showProgressDialog)
			{
				// Background Worker start mit Progressbar und Timer(eigener Dialog)
				// Der Dialog macht nix, er kommt jedes mal wenn eine Meldung erstellt wird
				// Reports aus dem MainWindow laden
			}
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
			GMaps.Instance.Mode = AccessMode.ServerAndCache;
			mapView.MapProvider = GoogleMapProvider.Instance;
			mapView.MinZoom = 2;
			mapView.MaxZoom = 17;
			mapView.Zoom = 2;
			mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
			mapView.CanDragMap = true;
			mapView.DragButton = MouseButton.Left;
		}
	}
}
