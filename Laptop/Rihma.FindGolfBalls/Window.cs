using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Rihma.FindGolfBalls
{
	public partial class Window : Form
	{
		private Capture _capture;
		private Gray cannyThreshold = new Gray(180);
		private Gray cannyThresholdLinking = new Gray(120);
		private Gray circleAccumulatorThreshold = new Gray(120);
		private Gray binaryThreshold = new Gray(100);
		private Gray binaryMaximumValue = new Gray(255);
		private Matrix<float> _homographyMatrix;

		public Window()
		{
			InitializeComponent();
			InitializeControls();

			const double twoPi = 2 * Math.PI;
			const string filename = @"C:\Temp\Rihma-Väljak-01.m4v";
			_capture = new Capture(filename);
			uxFilename.Text = filename;

			var timer = new Stopwatch();
			var timerCount = 0;
			long timeLast = 0;
			long timeNow = 0;
			long timeElapsed = 0;
			float fps = 0;

			timer.Start();
			var font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
			var smallFont = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);

			uxImage.Size = new Size(_capture.Width, _capture.Height);

			Application.Idle +=
				(sender, args) =>
				{
					// Loop video files
					if (_capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_AVI_RATIO) >= 1)
					{
						_capture = new Capture(filename);
						//return;
					}

					if (timer.IsRunning)
					{
						timerCount++;
						timeNow = timer.ElapsedMilliseconds;
						long timeDelta = timeNow - timeLast;
						timeElapsed += timeDelta;
						timeLast = timeNow;

						if (timeElapsed >= 1000)
						{
							fps = (float)timerCount / (timeElapsed / 1000);
							timerCount = 0;
							timeElapsed = 0;
						}
					}

					var img = _capture.QueryFrame();

					if (img == null) return;

					var gray = img.Convert<Gray, byte>();

					var chessBoard = img;
					if (ImageSource == ImageSource.Warped)
						chessBoard = FindChessboard(img, gray);

					gray = gray.PyrDown().PyrDown().PyrUp().PyrUp();					
					//gray._Dilate(2);
					//gray._Erode(2);
					//gray._EqualizeHist();
					var binary = gray.ThresholdBinary(binaryThreshold, binaryMaximumValue);
					//gray = gray.Canny(cannyThreshold, cannyThresholdLinking);

					var filteredImage = binary;
					var displayedImage = img;

					switch (ImageSource)
					{
						case ImageSource.Gray:
							displayedImage = gray.Convert<Bgr, byte>();
							break;
						case ImageSource.BlackAndWhite:
							displayedImage = binary.Convert<Bgr, byte>();
							break;
						case ImageSource.Warped:
							displayedImage = chessBoard.Copy();
							break;
					}

					using (var storage = new MemStorage())
					{
						var contours = filteredImage.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, RETR_TYPE.CV_RETR_EXTERNAL, storage);

						for (; contours != null; contours = contours.HNext)
						{
							var moments = contours.GetMoments();

							//var eccentricity = GetEccentricity(moments);

							var roundness = (contours.Perimeter * contours.Perimeter) / (twoPi * contours.Area);
							//if (roundness > 20) continue;

							var points = contours.Select(x => (PointF)x).ToArray();
							var circle = PointCollection.MinEnclosingCircle(points);

							if (roundness < 3)
								displayedImage.Draw(circle, new Bgr(Color.Green), 2);
							else
								displayedImage.Draw(contours, new Bgr(Color.Red), 1);

							//var cross = new Cross2DF(new PointF((float)moments.GravityCenter.x, (float)moments.GravityCenter.y), 10, 10);
							var cross = new Cross2DF(circle.Center, 10, 10);
							//gray.Draw(cross, new Gray(100), 2);
							displayedImage.Draw(cross, new Bgr(Color.Brown), 2);

							//img.Draw(contours.BoundingRectangle, new Bgr(Color.Blue), 1);
							displayedImage.Draw(string.Format("{0:0.0}", roundness), ref smallFont, contours.BoundingRectangle.Location, new Bgr(Color.WhiteSmoke));

							//gray.Draw(contours.BoundingRectangle, new Gray(70), 3);
							//gray.DrawPolyline(contours.ToArray(), false, new Gray(35), 2);
							//img.DrawPolyline(contours.ToArray(), false, new Bgr(Color.Gray), 2);
						}
					}

					displayedImage.Draw(string.Format("FPS: {0}", fps), ref font, new Point(10, 30), new Bgr(Color.White));

					uxImage.Image = displayedImage;
				};
		}

		public Image<Bgr, byte> FindChessboard(Image<Bgr, byte> img, Image<Gray, byte> gray)
		{
			var matrix = FindHomographyMatrix(img, gray);

			if (matrix != null)
				_homographyMatrix = matrix;

			if (_homographyMatrix == null)
				return img;

			Image<Bgr, Byte> rotated = img.WarpPerspective(
				_homographyMatrix,
				INTER.CV_INTER_LINEAR,
				WARP.CV_WARP_FILL_OUTLIERS, new Bgr(Color.Black));

			return rotated;
		}

		private Matrix<float> FindHomographyMatrix(Image<Bgr, byte> img, Image<Gray, byte> gray)
		{
			var patternSize = new Size(5, 8);
			PointF[] corners;

			var patternFound = CameraCalibration.FindChessboardCorners(
				gray,
				patternSize,
				CALIB_CB_TYPE.DEFAULT,
				out corners);

			gray.FindCornerSubPix(new PointF[][] { corners }, new Size(10, 10), new Size(-1, -1), new MCvTermCriteria(0.05));

			//var cornerCount = 0;
			//CvInvoke.cvDrawChessboardCorners(img.Ptr, patternSize, corners, cornerCount, patternFound ? 1 : 0);
			//CameraCalibration.DrawChessboardCorners(gray, patternSize, corners, patternFound);
			//gray.Save("chess2" + DateTime.Now.Ticks + ".jpg");
			//img.Save("chess1" + DateTime.Now.Ticks + (patternFound ? "Yes" : "No") + ".jpg");

			if (!patternFound) return null;

			var objPts = new PointF[4];
			var imgPts = new PointF[4];

			int width = 5;
			int height = 8;
			int wx = 450;
			int hy = 450;
			int lx = 400;
			int ly = 400;

			objPts[0] = new PointF(lx, ly);
			objPts[1] = new PointF(wx, ly);
			objPts[2] = new PointF(lx, hy);
			objPts[3] = new PointF(wx, hy);

			imgPts[0] = corners[0];
			imgPts[1] = corners[width - 1];
			imgPts[2] = corners[(height - 1) * width];
			imgPts[3] = corners[(height - 1) * width + width - 1];

			float[,] src =
			{
				{objPts[0].X, objPts[0].Y},
				{objPts[1].X, objPts[1].Y},
				{objPts[2].X, objPts[2].Y},
				{objPts[3].X, objPts[3].Y}
			};

			float[,] dest =
			{
				{imgPts[0].X, imgPts[0].Y},
				{imgPts[1].X, imgPts[1].Y},
				{imgPts[2].X, imgPts[2].Y},
				{imgPts[3].X, imgPts[3].Y}
			};

			Matrix<float> srcpm = new Matrix<float>(src);
			Matrix<float> dstpm = new Matrix<float>(dest);

			Matrix<float> homographyMatrix = CameraCalibration.FindHomography(
				dstpm, //points on the observed image
				srcpm, //points on the object image
				HOMOGRAPHY_METHOD.RANSAC,
				3
			).Convert<float>();

			return homographyMatrix;
		}

		public ImageSource ImageSource { get; set; }

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
			AddSlider("Binary Threshold", this, "BinaryThreshold", 0, 255);
			AddSlider("Binary Maximum Value", this, "BinaryMaximumValue", 0, 255);

			AddSlider("Threshold", this, "CannyThreshold", 0, 255);
			AddSlider("Threshold Linking", this, "CannyThresholdLinking", 0, 255);
			AddSlider("Circle Accumulator Threshold", this, "CannyCircleAccumulatorThreshold", 0, 255);

			AddSelection("Result color", typeof(ImageSource), Enum.GetNames(typeof(ImageSource)), this, "ImageSource");

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
			combobox.SelectedValueChanged += (sender, e) => {
				var value = Enum.Parse(dataType, combobox.SelectedItem.ToString());
				this.GetType().GetProperty(dataMember).SetValue(dataSource, value, null);
			};
			combobox.SelectedValue = this.GetType().GetProperty(dataMember).GetValue(dataSource, null);

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

			var result = dialog.ShowDialog(this);
			if (result != DialogResult.OK) return;

			uxFilename.Text = dialog.FileName;
			_capture = new Capture(dialog.FileName);
		}

		private void uxWebcam_Click(object sender, EventArgs e)
		{
			uxFilename.Clear();
			_capture = new Capture();
		}

		public int BinaryThreshold
		{
			get { return (int)binaryThreshold.Intensity; }
			set { binaryThreshold = new Gray(value); }
		}

		public int BinaryMaximumValue
		{
			get { return (int)binaryMaximumValue.Intensity; }
			set { binaryMaximumValue = new Gray(value); }
		}

		public decimal CannyThreshold
		{
			get { return (decimal)cannyThreshold.Intensity; }
			set { cannyThreshold = new Gray((double)value); }
		}

		public decimal CannyThresholdLinking
		{
			get { return (decimal)cannyThresholdLinking.Intensity; }
			set { cannyThresholdLinking = new Gray((double)value); }
		}

		public decimal CannyCircleAccumulatorThreshold
		{
			get { return (decimal)circleAccumulatorThreshold.Intensity; }
			set { circleAccumulatorThreshold = new Gray((double)value); }
		}
	}

	public enum ImageSource
	{
		RGB,
		Gray,
		BlackAndWhite,
		Warped
	}
}