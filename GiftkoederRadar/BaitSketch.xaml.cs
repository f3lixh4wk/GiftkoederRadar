using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace GiftkoederRadar
{
	/// <summary>
	/// Interaktionslogik für BaitSketch.xaml
	/// </summary>
	public partial class BaitSketch : Window
	{
		public BaitSketch()
		{
			InitializeComponent();
		}

		private void canvasMouseEnter(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Pen;
		}

		private void canvasMouseLeave(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Arrow;
		}

		private void canvasMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
				currentPoint = e.GetPosition(this);
		}

		private void canvasMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				//2.3.3.In ein Canvas dynamisch zeichnen (wie in Aufgabe 8.2)
				Line line = new Line();

				line.Stroke = SystemColors.WindowFrameBrush;
				line.X1 = currentPoint.X;
				line.Y1 = currentPoint.Y;
				line.X2 = e.GetPosition(this).X;
				line.Y2 = e.GetPosition(this).Y;

				currentPoint = e.GetPosition(this);
				paintArea.Children.Add(line);
			}
		}

		private void btnClick(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;

			if (btn == btnAddSketchToReport)
			{
				closeDialog = true;
				DialogResult = true; // calls baitSketchClosing
				SketchFileName = GetNextFreeFileName();
				createBitmapFromCanvas();
			}
			else if (btn == btnCancel)
			{
				if (paintArea.Children.Count > 0)
				{
					MessageBoxResult dialogResult = MessageBox.Show
					(
						"Möchtest Sie die Skizze verwerfen?", "Skizzieren Abbrechen",
						MessageBoxButton.YesNo, MessageBoxImage.Warning
					);

					// Der User möchte die Skizze verwerfen
					if (dialogResult == MessageBoxResult.Yes)
						closeDialog = true;

					// Der User möchte die Skizze nicht verwerfen
					else if (dialogResult == MessageBoxResult.No)
						return;
				}
				// Der User hat keine Skizze gemacht und möchte Abbrechen
				Close();
			}
		}

		private void baitSketchClosing(object sender, CancelEventArgs e)
		{
			if (closeDialog)
				return;

			if (paintArea.Children.Count > 0)
			{
				MessageBoxResult dialogResult = MessageBox.Show
				(
					"Möchten Sie die Skizze verwerfen?", "Skizzieren Abbrechen",
					MessageBoxButton.YesNo, MessageBoxImage.Warning
				);

				// Der User möchte die Skizze verwerfen
				if (dialogResult == MessageBoxResult.Yes)
					DialogResult = false;

				// Der User möchte die Skizze nicht verwerfen
				else if (dialogResult == MessageBoxResult.No)
					e.Cancel = true;
			}
		}

		void createBitmapFromCanvas()
		{
			Rect bounds = VisualTreeHelper.GetDescendantBounds(paintArea);
			RenderTargetBitmap rtb = new RenderTargetBitmap
			(
				(Int32)bounds.Width,
				(Int32)bounds.Height,
				96, 96,
				PixelFormats.Pbgra32
			);

			DrawingVisual dv = new DrawingVisual();
			using (DrawingContext dc = dv.RenderOpen())
			{
				VisualBrush vb = new VisualBrush(paintArea);
				dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
			}

			rtb.Render(dv);

			PngBitmapEncoder sketch = new PngBitmapEncoder();
			sketch.Frames.Add(BitmapFrame.Create(rtb));
			using (Stream stm = File.Create(SketchFileName))
				sketch.Save(stm);
		}

		private string GetNextFreeFileName()
		{
			string sketchDirPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			int nextFreeFileIndex = 0;
			string sketchFilePath = sketchDirPath + "\\sketch" + nextFreeFileIndex.ToString() + pngSuffix;
			while(File.Exists(sketchFilePath))
			{
				++nextFreeFileIndex;
				sketchFilePath = sketchDirPath + "\\sketch" + nextFreeFileIndex.ToString() + pngSuffix;
			}
			return sketchFilePath;
		}
		public string SketchFileName { get; set; }

		private bool closeDialog = false;
		private Point currentPoint;
		private string pngSuffix = ".png";
	}
}
