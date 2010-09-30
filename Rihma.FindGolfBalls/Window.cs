using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Rihma.ComputerVisionUI
{
    public partial class Window : Form
    {
        private Capture _capture;
        private Gray cannyThreshold = new Gray(180);
        private Gray cannyThresholdLinking = new Gray(120);
        private Gray circleAccumulatorThreshold = new Gray(120);
        private Gray binaryThreshold = new Gray(100);
        private Gray binaryMaximumValue = new Gray(255);

        public Window()
        {
            InitializeComponent();
            InitializeControls();

            const double twoPi = 2 * Math.PI;
            const string filename = "C:\\Temp\\capture2-15.avi";
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

                        var gray = img.Convert<Gray, byte>().PyrDown().PyrDown().PyrUp().PyrUp();
                        //gray._Dilate(2);
                        //gray._Erode(2);
                        //gray._EqualizeHist();
                        gray._ThresholdBinary(binaryThreshold, binaryMaximumValue);
                        //gray = gray.Canny(cannyThreshold, cannyThresholdLinking);

                        using (var storage = new MemStorage())
                        {
                            var contours = gray.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, RETR_TYPE.CV_RETR_EXTERNAL, storage);

                            for (; contours != null; contours = contours.HNext)
                            {
                                var moments = contours.GetMoments();

                                //var eccentricity = GetEccentricity(moments);

                                var roundness = (contours.Perimeter * contours.Perimeter) / (twoPi * contours.Area);
                                //if (roundness > 20) continue;

                                var points = contours.Select(x => (PointF)x).ToArray();
                                var circle = PointCollection.MinEnclosingCircle(points);

                                if (roundness < 3)
                                    img.Draw(circle, new Bgr(Color.Green), 2);
                                else
                                    img.Draw(contours.BoundingRectangle, new Bgr(Color.Red), 1);

                                //var cross = new Cross2DF(new PointF((float)moments.GravityCenter.x, (float)moments.GravityCenter.y), 10, 10);
                                var cross = new Cross2DF(circle.Center, 10, 10);
                                //gray.Draw(cross, new Gray(100), 2);
                                img.Draw(cross, new Bgr(Color.Brown), 2);

                                //img.Draw(contours.BoundingRectangle, new Bgr(Color.Blue), 1);
                                img.Draw(string.Format("{0:0.0}", roundness), ref smallFont, contours.BoundingRectangle.Location, new Bgr(Color.WhiteSmoke));

                                //gray.Draw(contours.BoundingRectangle, new Gray(70), 3);
                                //gray.DrawPolyline(contours.ToArray(), false, new Gray(35), 2);
                                //img.DrawPolyline(contours.ToArray(), false, new Bgr(Color.Gray), 2);
                            }
                        }

                        //gray.Draw(string.Format("FPS: {0}", fps), ref font, new Point(10, 30), new Gray(255));
                        img.Draw(string.Format("FPS: {0}", fps), ref font, new Point(10, 30), new Bgr(Color.White));

                        //uxImage.Image = gray;
                        uxImage.Image = img;
                    };
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
            AddSlider("Binary Threshold", this, "BinaryThreshold", 0, 255);
            AddSlider("Binary Maximum Value", this, "BinaryMaximumValue", 0, 255);

            AddSlider("Threshold", this, "CannyThreshold", 0, 255);
            AddSlider("Threshold Linking", this, "CannyThresholdLinking", 0, 255);
            AddSlider("Circle Accumulator Threshold", this, "CannyCircleAccumulatorThreshold", 0, 255);
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

        private void uxFilenameBrowse_Click(object sender, EventArgs e)
        {
            var dialog =
                    new OpenFileDialog
                    {
                        DefaultExt = "avi",
                        Filter = "Video files (*.avi)|*.avi|All files (*.*)|*.*"
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
}