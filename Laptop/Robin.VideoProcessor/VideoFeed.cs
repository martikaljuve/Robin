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
		private Image<Bgr, byte> frame;
		private readonly VideoCaptureDevice videoSource;
		private readonly FileVideoSource fileSource;

		private int framesPerSecond;

		public VideoFeed(string filename)
		{
			fileSource = new FileVideoSource(filename);
			fileSource.NewFrame += VideoSourceOnNewFrame;
			fileSource.Start();

			var timer = new Timer(1000);
			timer.Elapsed += (sender, args) => framesPerSecond = fileSource.FramesReceived;
			timer.Start();
		}

		public VideoFeed(int camIndex = 0)
		{
			var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			videoSource = new VideoCaptureDevice(videoDevices[camIndex].MonikerString);

			//videoSource.DesiredFrameRate = 60;
			//videoSource.DesiredFrameSize = new Size(320, 240);

			videoSource.NewFrame += VideoSourceOnNewFrame;
			videoSource.Start();

			var timer = new Timer(1000);
			timer.Elapsed += (sender, args) => framesPerSecond = videoSource.FramesReceived;
			timer.Start();
		}

		private void VideoSourceOnNewFrame(object sender, NewFrameEventArgs eventArgs)
		{
			frame = VisionExperiments.HoughCircles(eventArgs.Frame);

			var result = frame;

			//frame = new Image<Bgr, byte>(eventArgs.Frame);
			
			//var result = VisionExperiments.GetAverageForSubRectangle(frame);
			//var result = VisionExperiments.FilterByColor(frame);
			//var result = VisionExperiments.CannyEdges(frame);

			//var result = camshift.Track(frame);

			// Draw fps
			result.Draw(framesPerSecond.ToString(), ref VisionExperiments.Font, new Point(frame.Width - 50, 20), new Bgr(Color.YellowGreen));

			OnFrameProcessed(new FrameEventArgs(result));
		}

		public event EventHandler<FrameEventArgs> FrameProcessed;

		public void OnFrameProcessed(FrameEventArgs eventargs)
		{
			var handler = FrameProcessed;
			if (handler != null)
				handler(this, eventargs);
		}

		public void SetRegionOfInterest(Rectangle rect)
		{
			camshift.CalculateHistogram(frame, rect);
		}

		public void ProcessKeyCommand(char key)
		{
			camshift.SendKeyCommand(key);
		}

		public void Stop()
		{
			if (videoSource != null) {
				videoSource.SignalToStop();
				videoSource.WaitForStop();
			}

			if (fileSource != null)
			{
				fileSource.SignalToStop();
				fileSource.WaitForStop();
			}
		}
	}
}
