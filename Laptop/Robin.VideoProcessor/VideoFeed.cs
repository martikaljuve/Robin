using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VideoFeed
	{
		private readonly string filename = null;
		//private const string Filename = @"C:\Temp\capture2-16.avi";
		//private const string Filename = @"C:\Temp\RobinFieldTest.wmv";
		//private const string Filename = @"C:\Temp\Rihma-Proov-01.m4v";

		private Capture capture;

		private readonly Camshift camshift = new Camshift();
		private Image<Bgr, byte> frame;

		public VideoFeed(int camIndex)
		{
			capture = new Capture(camIndex);
		}

		public VideoFeed(string filename = null)
		{
			this.filename = filename;
			capture = filename == null ? new Capture() : new Capture(filename);
		}
		
		public Image<Bgr, byte> CaptureFrame()
		{
			if (capture == null)
				return null;

			// HACK: Loop video files

			var aviRatio = CvInvoke.cvGetCaptureProperty(capture, CAP_PROP.CV_CAP_PROP_POS_AVI_RATIO);
			if (aviRatio >= 1.0)
				capture = new Capture(filename);

			frame = capture.QueryFrame();

			if (frame == null)
				return null;

			//frame = VisionExperiments.GetAverageForSubRectangle(frame);
			//frame = VisionExperiments.FilterByColor(frame);
			
			frame = camshift.Track(frame);
			
			return frame;
		}

		public void SetRegionOfInterest(Rectangle rect)
		{
			camshift.CalculateHistogram(frame, rect);
		}
	}
}
