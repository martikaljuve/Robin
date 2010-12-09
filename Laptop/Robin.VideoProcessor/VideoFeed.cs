using System;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VideoFeed
	{
		public const string Sample1 = @"C:\Temp\capture26.avi";
		public const string Sample2 = @"C:\Temp\Video 7.wmv";
		public const string Sample3 = @"C:\Temp\Rihma-Proov-01.m4v";
		public const string Sample4 = @"C:\Temp\robin-2010-11-06-01.avi";
		public const string Sample5 = @"C:\Temp\robin-2010-11-06-02.avi";
		public const string Sample6 = @"C:\Temp\robin-2010-11-06-03.avi";
		public const string Sample7 = @"C:\Temp\Video 10.wmv";

		private readonly IVideoSource videoSource;

		public VideoFeed(string filename)
			: this(new FileVideoSource(filename)) { }
		
		private VideoFeed(IVideoSource source)
		{
			videoSource = source;

			//videoSource.DesiredFrameRate = 60;
			//videoSource.DesiredFrameSize = new Size(640, 480);

			videoSource.Start();
		}

		public static VideoFeed FromCamIndex(int camIndex) {
			var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			if (videoDevices.Count == 0)
				return null;

			return new VideoFeed(new VideoCaptureDevice(videoDevices[camIndex].MonikerString));
		}

		public event NewFrameEventHandler NewFrame
		{
			add { videoSource.NewFrame += value; }
			remove { videoSource.NewFrame -= value; }
		}

		public void Start()
		{
			if (videoSource != null)
				videoSource.Start();
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

		public int FramesReceived
		{
			get { return videoSource.FramesReceived; }
		}
	}

	public class VideoFeed2
	{
		private Capture capture;
		public VideoFeed2(int camIndex)
		{
			capture = new Capture(camIndex);
		}

		public VideoFeed2(string filename)
		{
			capture = new Capture(filename);
		}

		public Image<Bgr, byte> Query()
		{
			return capture.QueryFrame();
		}

		public void Stop()
		{
			
		}

		public void Restart()
		{
			if (capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_AVI_RATIO) > 0.9)
				capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_AVI_RATIO, 0.0);
			
		}
	}
}