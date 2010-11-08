using AForge.Video;
using AForge.Video.DirectShow;

namespace Robin.VideoProcessor
{
	public class VideoFeed
	{
		public const string Sample1 = @"C:\Temp\capture2-16.avi";
		public const string Sample2 = @"C:\Temp\RobinFieldTest.avi";
		public const string Sample3 = @"C:\Temp\Rihma-Proov-01.m4v";
		public const string Sample4 = @"C:\Temp\robin-2010-11-06-01.avi";
		public const string Sample5 = @"C:\Temp\robin-2010-11-06-02.avi";
		public const string Sample6 = @"C:\Temp\robin-2010-11-06-03.avi";

		private readonly IVideoSource videoSource;

		public VideoFeed(string filename)
			: this(new FileVideoSource(filename)) { }

		public VideoFeed(int camIndex = 0)
			: this(new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[camIndex].MonikerString)) { }

		private VideoFeed(IVideoSource source)
		{
			videoSource = source;

			//videoSource.DesiredFrameRate = 60;
			//videoSource.DesiredFrameSize = new Size(640, 480);

			videoSource.Start();
		}

		public event NewFrameEventHandler NewFrame
		{
			add { videoSource.NewFrame += value; }
			remove { videoSource.NewFrame -= value; }
		}

		public void Start()
		{
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
}
