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
		//private const string _filename = @"C:\Temp\Rihma-Väljak-01.m4v";
		private const string _filename = @"C:\Temp\Rihma-01.jpg";

		private readonly Stopwatch _timer = new Stopwatch();
		private Capture _capture;
		private float _fps;
		private long _timeElapsed;
		private long _timeLast;
		private long _timeNow;
		private long _timerCount;
		private Gray binaryMaximumValue = new Gray(255);
		private Gray binaryThreshold = new Gray(100);

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

		[Import]
		public IImageProcessor ImageProcessor { get; set; }

		public ImageSource ImageSource { get; set; }

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

			Image<Bgr, byte> displayedImage = img.Copy();

			var size = img.Size;
			var sum = img.GetSubRect(new Rectangle(20, size.Height - 20, 10, 10)).GetSum();

			if (ImageProcessor != null)
				ImageProcessor.Process(img, ref displayedImage);

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
		
		private void InitializeControls()
		{
			uxTable.SuspendLayout();
			//uxImageProcessor.DataSource = ImageProcessors;
			//uxImageProcessor.DisplayMember = "Name";
			//uxImageProcessor.SelectedIndexChanged +=
			//    (sender, args) => SelectedProcessor = ImageProcessors[uxImageProcessor.SelectedIndex];
			//SelectedProcessor = ImageProcessors.First();

			//AddSlider("Binary Threshold", this, "BinaryThreshold", 0, 255);
			//AddSlider("Binary Maximum Value", this, "BinaryMaximumValue", 0, 255);

			//AddSlider("Threshold", this, "CannyThreshold", 0, 255);
			//AddSlider("Threshold Linking", this, "CannyThresholdLinking", 0, 255);
			//AddSlider("Circle Accumulator Threshold", this, "CannyCircleAccumulatorThreshold", 0, 255);

			//AddSelection("Result color", typeof (ImageSource), Enum.GetNames(typeof (ImageSource)), this, "ImageSource");

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