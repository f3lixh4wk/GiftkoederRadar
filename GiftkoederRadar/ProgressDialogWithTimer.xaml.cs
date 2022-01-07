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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GiftkoederRadar
{
	/// <summary>
	/// Interaktionslogik für ProgressDialogWithTimer.xaml
	/// </summary>
	public partial class ProgressDialogWithTimer : Window
	{
		static List<string> currentOperations = new List<string>
		{
			"Meldung wird erstellt...",
			"Radar wird geladen...",
			"Meldungen werden aktualisiert..."
		};

		BackgroundWorker backgroundWorker = new BackgroundWorker();

		public ProgressDialogWithTimer(int secondsToWait)
		{
			InitializeComponent();

			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
			backgroundWorker.DoWork += backgroundWorker_DoWork;
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.WorkerSupportsCancellation = false;
			waitingTimeInSeconds = secondsToWait;

			backgroundWorker.RunWorkerAsync();

			DispatcherTimer countTimer = null;
			TimeSpan countDown;
			countDown = TimeSpan.FromSeconds(5);
			countTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
			{
				lblRemainingTime.Content = countDown.ToString();
				if (countDown == TimeSpan.Zero) countTimer.Stop();
						 countDown = countDown.Add(TimeSpan.FromSeconds(-1));
			}, Application.Current.Dispatcher);
			countTimer.Start();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;

			for (int second = 0; second <= waitingTimeInSeconds; second++)
			{
				System.Threading.Thread.Sleep(1000); // 1 sec
				worker.ReportProgress(second);
			}
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pbProgress.Value = e.ProgressPercentage;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Close();
		}

		private int waitingTimeInSeconds;
	}
}
