using System;
using System.Drawing;
using System.Timers;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VideoFeed
	{
		private readonly string filename;
		public const string Sample1 = @"C:\Temp\capture2-16.avi";
		public const string Sample2 = @"C:\Temp\RobinFieldTest.avi";
		public const string Sample3 = @"C:\Temp\Rihma-Proov-01.m4v";

		private readonly Camshift camshift = new Camshift();
		private readonly IVideoSource videoSource;

		private int framesPerSecond;

		public VideoFeed(string filename)
			: this(new FileVideoSource(filename)) { }

		public VideoFeed(int camIndex = 0)
			: this(new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[camIndex].MonikerString)) { }

		private VideoFeed(IVideoSource source)
		{
			videoSource = source;
			//videoSource.DesiredFrameRate = 60;
			//videoSource.DesiredFrameSize = new Size(640, 480);

			videoSource.NewFrame += VideoSourceOnNewFrame;
			videoSource.Start();

			var timer = new Timer(1000);
			timer.Elapsed += (sender, args) => framesPerSecond = videoSource.FramesReceived;
			timer.Start();
		}

		private void VideoSourceOnNewFrame(object sender, NewFrameEventArgs eventArgs)
		{
			//frame = VisionExperiments.HoughCircles(eventArgs.Frame);
			var result = VisionExperiments.FindGolfBallsWithHoughAndColorFilter(eventArgs.Frame);

			//var result = VisionExperiments.GetAverageForSubRectangle(frame);
			//var result = VisionExperiments.FilterByColor(frame);
			//var result = VisionExperiments.CannyEdges(frame);
			//var result = camshift.Track(frame);
			
			using (var g = Graphics.FromImage(result))
			{
				var fps = string.Format("FPS: {0}", framesPerSecond);
				g.DrawString(fps, SystemFonts.DefaultFont, Brushes.YellowGreen, new Point(result.Width - 50, 10));

				g.FillRectangle(Brushes.White, 5, 5, 100, 50);
				g.DrawString("Threshold: " + VisionExperiments.Threshold, SystemFonts.DefaultFont, Brushes.Crimson, new PointF(10, 10));
				g.DrawString("Linking: " + VisionExperiments.ThresholdLinking, SystemFonts.DefaultFont, Brushes.Crimson, new PointF(10, 30));

				if (showCircles)
					foreach (var circle in VisionExperiments.Circles)
					{
						g.DrawEllipse(ellipsePen, circle.X - circle.Radius, circle.Y - circle.Radius, circle.Radius*2, circle.Radius*2);
						g.DrawString(circle.Intensity.ToString(), SystemFonts.DefaultFont, Brushes.Orange, circle.X, circle.Y);
					}
			}

			OnFrameProcessed(new FrameEventArgs(result));
		}

		private Pen ellipsePen = new Pen(Color.Fuchsia, 2);

		public event EventHandler<FrameEventArgs> FrameProcessed;

		public void OnFrameProcessed(FrameEventArgs eventargs)
		{
			var handler = FrameProcessed;
			if (handler != null)
				handler(this, eventargs);
		}

		public void SetRegionOfInterest(Rectangle rect)
		{
			//camshift.CalculateHistogram(frame, rect);
		}

		private static bool showCircles;
		public void ProcessKeyCommand(char key)
		{
			if (key == 'c')
				showCircles = !showCircles;

			camshift.SendKeyCommand(key);
		}

		public void Stop()
		{
			if (videoSource == null) return;

			videoSource.SignalToStop();
			videoSource.WaitForStop();
		}

		public void Restart()
		{
			if (videoSource == null) return;
			videoSource.SignalToStop();
			videoSource.WaitForStop();
			videoSource.Start();
		}
	}
}
