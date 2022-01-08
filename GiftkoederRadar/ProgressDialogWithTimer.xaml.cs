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

		//1.7. Backgroundworker
		BackgroundWorker backgroundWorker = new BackgroundWorker();

		public ProgressDialogWithTimer(int millisecondsToWait)
		{
			InitializeComponent();

			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
			backgroundWorker.DoWork += backgroundWorker_DoWork;
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.WorkerSupportsCancellation = false;
			waitingTimeInMilliseconds = millisecondsToWait;

			pbProgress.Maximum = millisecondsToWait;
			backgroundWorker.RunWorkerAsync();

			//2.3.4.Mit Timer arbeiten (wie in Aufgabe 4.2)
			DispatcherTimer countTimer = null;
			TimeSpan countDown;
			countDown = TimeSpan.FromSeconds(6);
			countTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
			  {
				  lblRemainingTime.Content = "Verbleibende Zeit: " + countDown.Seconds + " Sekunden";
				  if (countDown.Seconds == 6)
					  lblCurrentOperation.Content = currentOperations[0];
				  if (countDown.Seconds == 4)
					  lblCurrentOperation.Content = currentOperations[1];
				  if (countDown.Seconds == 2)
					  lblCurrentOperation.Content = currentOperations[2];
				  if (countDown == TimeSpan.Zero)
					  countTimer.Stop();

				  countDown = countDown.Add(TimeSpan.FromSeconds(-1));
			  }, Application.Current.Dispatcher);
			countTimer.Start();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
			for (int millisecond = 0; millisecond <= waitingTimeInMilliseconds; millisecond++)
			{
				System.Threading.Thread.Sleep(1);
				worker.ReportProgress(millisecond);
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

		private int waitingTimeInMilliseconds;
	}
}
