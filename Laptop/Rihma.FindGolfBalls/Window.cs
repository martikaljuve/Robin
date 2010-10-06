using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Linq;

namespace Rihma.FindGolfBalls
{
	public partial class Window : Form
	{
		private const string _filename = @"C:\Temp\Rihma-Väljak-01.m4v";

		private readonly Stopwatch _timer = new Stopwatch();
		private Capture _capture;
		private float _fps;
		private long _timeElapsed;
		private long _timeLast;
		private long _timeNow;
		private long _timerCount;
		private Gray binaryMaximumValue = new Gray(255);
		private Gray binaryThreshold = new Gray(100);
		private Gray cannyThreshold = new Gray(180);
		private Gray cannyThresholdLinking = new Gray(120);
		private Gray circleAccumulatorThreshold = new Gray(120);

		public Window()
		{
			Compose();
			InitializeComponent();
			InitializeControls();

			_capture = new Capture(_filename);
			uxFilename.Text = _filename;

			_timer.Start();

			uxImage.Size = new Size(_capture.Width, _capture.Height);

			Application.Idle += ApplicationOnIdle;
		}

		[ImportMany]
		public List<IImageProcessor> ImageProcessors { get; set; }

		public IImageProcessor SelectedProcessor { get; set; }
		public ImageSource ImageSource { get; set; }

		public int BinaryThreshold
		{
			get { return (int) binaryThreshold.Intensity; }
			set { binaryThreshold = new Gray(value); }
		}

		public int BinaryMaximumValue
		{
			get { return (int) binaryMaximumValue.Intensity; }
			set { binaryMaximumValue = new Gray(value); }
		}

		public decimal CannyThreshold
		{
			get { return (decimal) cannyThreshold.Intensity; }
			set { cannyThreshold = new Gray((double) value); }
		}

		public decimal CannyThresholdLinking
		{
			get { return (decimal) cannyThresholdLinking.Intensity; }
			set { cannyThresholdLinking = new Gray((double) value); }
		}

		public decimal CannyCircleAccumulatorThreshold
		{
			get { return (decimal) circleAccumulatorThreshold.Intensity; }
			set { circleAccumulatorThreshold = new Gray((double) value); }
		}

		private void ApplicationOnIdle(object sender, EventArgs eventArgs)
		{
			// Loop video files
			if (_capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_AVI_RATIO) >= 1)
				_capture = new Capture(_filename);

			if (_timer.IsRunning)
			{
				_timerCount++;
				_timeNow = _timer.ElapsedMilliseconds;
				long timeDelta = _timeNow - _timeLast;
				_timeElapsed += timeDelta;
				_timeLast = _timeNow;

				if (_timeElapsed >= 1000)
				{
					_fps = _timerCount/(_timeElapsed/1000f);
					_timerCount = 0;
					_timeElapsed = 0;
				}
			}

			var img = _capture.QueryFrame();

			if (img == null)
				return;

			var gray = img.Convert<Gray, byte>();

			//gray._Dilate(2);
			//gray._Erode(2);

			//gray = gray.Canny(cannyThreshold, cannyThresholdLinking);

			Image<Bgr, byte> displayedImage;

			switch (ImageSource)
			{
				case ImageSource.Gray:
					displayedImage = gray.Convert<Bgr, byte>();
					break;
				case ImageSource.BlackAndWhite:
					displayedImage = gray.ThresholdBinary(binaryThreshold, binaryMaximumValue).Convert<Bgr, byte>();
					break;
				default:
					displayedImage = img.Copy();
					break;
			}

			var size = img.Size;
			var sum = img.GetSubRect(new Rectangle(20, size.Height - 20, 10, 10)).GetSum();

			if (SelectedProcessor != null)
				SelectedProcessor.Process(img, ref displayedImage);

			displayedImage.Draw(string.Format("FPS: {0:0}", _fps), ref EmguHelper.NormalFont, new Point(10, 30),
			                    new Bgr(Color.White));

			uxImage.Image = displayedImage;
		}

		private void Compose()
		{
			var assemblyCatalog = new AssemblyCatalog(typeof (GolfBallFinder).Assembly);
			var directoryCatalog = new DirectoryCatalog(@".\");
			var catalog = new AggregateCatalog(assemblyCatalog, directoryCatalog);

			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}

		//private static double GetEccentricity(MCvMoments moments) {
		//    var m1 = moments.GetCentralMoment(2, 0);
		//    var m2 = moments.GetCentralMoment(0, 2);
		//    var m11 = m1 - m2;
		//    var m22 = m1 + m2;
		//    var m3 = moments.GetCentralMoment(1, 1);
		//    return ((m11*m11) - 4*(m3*m3)) / (m22 * m22);
		//}

		private void InitializeControls()
		{
			uxTable.SuspendLayout();
			uxImageProcessor.DataSource = ImageProcessors;
			uxImageProcessor.DisplayMember = "Name";
			uxImageProcessor.SelectedIndexChanged +=
				(sender, args) => SelectedProcessor = ImageProcessors[uxImageProcessor.SelectedIndex];
			SelectedProcessor = ImageProcessors.First();

			AddSlider("Binary Threshold", this, "BinaryThreshold", 0, 255);
			AddSlider("Binary Maximum Value", this, "BinaryMaximumValue", 0, 255);

			AddSlider("Threshold", this, "CannyThreshold", 0, 255);
			AddSlider("Threshold Linking", this, "CannyThresholdLinking", 0, 255);
			AddSlider("Circle Accumulator Threshold", this, "CannyCircleAccumulatorThreshold", 0, 255);

			AddSelection("Result color", typeof (ImageSource), Enum.GetNames(typeof (ImageSource)), this, "ImageSource");

			uxTable.ResumeLayout(true);
		}

		private void AddSlider(string text, object dataSource, string dataMember, int min, int max)
		{
			int row = uxTable.RowCount++ - 1;
			uxTable.RowStyles.Insert(row, new RowStyle(SizeType.Absolute, 30));

			var label = new Label();
			label.Text = text;
			label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

			var slider = new TrackBar();
			slider.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			slider.Minimum = min;
			slider.Maximum = max;
			slider.DataBindings.Add("Value", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);

			var textbox = new TextBox();
			textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			textbox.DataBindings.Add("Text", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);

			uxTable.Controls.Add(label, 0, row);
			uxTable.Controls.Add(slider, 1, row);
			uxTable.Controls.Add(textbox, 2, row);
		}

		private void AddSelection(string text, Type dataType, object[] items, object dataSource, string dataMember)
		{
			int row = uxTable.RowCount++ - 1;
			uxTable.RowStyles.Insert(row, new RowStyle(SizeType.Absolute, 30));

			var label = new Label();
			label.Text = text;
			label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

			var combobox = new ComboBox();
			combobox.DropDownStyle = ComboBoxStyle.DropDownList;
			combobox.DataSource = items;
			//combobox.DataBindings.Add("SelectedValue", dataSource, dataMember, true);
			combobox.SelectedValueChanged += (sender, e) =>
			                                 {
			                                 	object value = Enum.Parse(dataType, combobox.SelectedItem.ToString());
			                                 	GetType().GetProperty(dataMember).SetValue(dataSource, value, null);
			                                 };
			combobox.SelectedValue = GetType().GetProperty(dataMember).GetValue(dataSource, null);

			uxTable.Controls.Add(label, 0, row);
			uxTable.Controls.Add(combobox, 1, row);

			uxTable.SetColumnSpan(combobox, 2);
		}

		private void uxFilenameBrowse_Click(object sender, EventArgs e)
		{
			var dialog =
				new OpenFileDialog
				{
					DefaultExt = "avi",
					Filter = "Video files (*.avi, *.m4v)|*.avi;*.m4v|All files (*.*)|*.*"
				};

			DialogResult result = dialog.ShowDialog(this);
			if (result != DialogResult.OK) return;

			uxFilename.Text = dialog.FileName;
			_capture = new Capture(dialog.FileName);
		}

		private void uxWebcam_Click(object sender, EventArgs e)
		{
			uxFilename.Clear();
			_capture = new Capture();
		}
	}
}