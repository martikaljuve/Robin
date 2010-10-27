using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class Camshift
	{
		private DenseHistogram histogram;

		private Image<Gray, byte> backProjection;

		private Rectangle rectangle;
		private Rectangle trackWindow;
		private MCvConnectedComp trackComp;
		private MCvBox2D trackBox;

		private Hsv maskLower = new Hsv(0, 0, 200);
		private Hsv maskHigher = new Hsv(360, 360, 360);
		private readonly Size frameSize = new Size(640, 480);
		
		public Camshift()
		{
			//trackWindow = new Rectangle(295, 90, 25, 25);
			//trackWindow = new Rectangle(262, 90, 25, 25);

			backProjection = new Image<Gray, byte>(frameSize);
		}

		public void CalculateHistogram(Image<Bgr, byte> frame, Rectangle rect)
		{
			histogram = new DenseHistogram(16, new RangeF(0, 180));
			trackWindow = rect;
			rectangle = rect;

			using (var ball = frame.GetSubRect(rect)) { 
				var hsv = ball.Convert<Hsv, byte>();
				//hsv._EqualizeHist();

				maskLower = hsv.GetAverage();
				maskHigher = hsv.GetAverage();

				const int hueThreshold = 30;
				const int saturationThreshold = 30;
				const int valueThreshold = 30;

				maskLower.Hue -= hueThreshold;
				maskHigher.Hue += hueThreshold;

				maskLower.Satuation -= saturationThreshold;
				maskHigher.Satuation += saturationThreshold;

				maskLower.Value -= valueThreshold;
				maskHigher.Value += valueThreshold;

				var mask = hsv.InRange(maskLower, maskHigher);

				var channels = hsv.Split();
				var hue = channels[0];

				var images = new[] {hue.Ptr};
				CvInvoke.cvCalcHist(images, histogram.Ptr, false, mask.Ptr);
			}
		}

		public Image<Bgr, byte> Track(Image<Bgr, byte> frame)
		{
			if (histogram == null) {
				frame.Draw("Not tracking", ref VisionExperiments.Font, new Point(30, 20), new Bgr(Color.Yellow));
				return frame;
			}
			 
			var hsv = frame.Convert<Hsv, byte>();
			var mask = hsv.InRange(maskLower, maskHigher);
			
			var channels = hsv.Split();
			var hue = channels[0];

			var images = new[] {hue.Ptr};
			CvInvoke.cvCalcBackProject(images, backProjection.Ptr, histogram.Ptr);
			backProjection = backProjection.And(mask);

			//var grayFrame = frame.Convert<Gray, byte>();
			//grayFrame._EqualizeHist();

			if (trackWindow.Width == 0) trackWindow.Width = 10;
			if (trackWindow.Height == 0) trackWindow.Height = 10;

			CvInvoke.cvCamShift(backProjection.Ptr, trackWindow, new MCvTermCriteria(10, 0.1), out trackComp, out trackBox);
			trackWindow = trackComp.rect;

			//frame = mask.Convert<Bgr, byte>();
			//frame = backProjection.Convert<Bgr, byte>();

			frame.Draw(rectangle, new Bgr(Color.DarkCyan), 2);
			frame.Draw("Tracking", ref VisionExperiments.Font, new Point(60, 50), new Bgr(Color.Yellow));
			frame.Draw(trackWindow, new Bgr(Color.Red), 3);

			return frame;
		}
	}
}