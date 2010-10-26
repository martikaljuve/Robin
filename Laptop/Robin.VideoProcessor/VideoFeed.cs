using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VideoFeed
	{
		private Capture capture;
		private const string Filename = @"C:\Temp\Rihma-Väljak-01.m4v";

		public VideoFeed(Capture capture = null)
		{
			this.capture = capture ?? new Capture(Filename);

			//this.capture.QueryFrame();
		}

		public Image<Bgr, byte> CaptureFrame()
		{
			if (capture == null)
				return null;

			// HACK: Loop video files

			//var aviRatio = CvInvoke.cvGetCaptureProperty(capture, CAP_PROP.CV_CAP_PROP_POS_FRAMES);
			//if (aviRatio >= 1.0)
			//	capture = new Capture(Filename);

			var frame = capture.QueryFrame();

			if (frame == null)
				return null;

			
			//frame = frame.PyrDown().PyrDown().PyrDown().PyrUp().PyrUp().PyrUp();


			return frame;
		}
	}
}
