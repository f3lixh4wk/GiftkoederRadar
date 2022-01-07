using System;
using System.Collections.Generic;
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
	/// Interaktionslogik für StartView.xaml
	/// </summary>
	public partial class StartView : Page
	{
		public StartView()
		{
			InitializeComponent();
		}

		private void btnClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			if (btn == btnGiftKoederKarte)
			{
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.MapView);
				mainWindow.ChangeView(new MapView());
			}
			else if (btn == btnGiftKoederMelden)
			{
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				mainWindow.SetActiveView(View.ReportView);
				mainWindow.ChangeView(new ReportView(View.StartView));
			}
		}
	}
}
