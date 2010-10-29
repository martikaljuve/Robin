using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Linq;

namespace Robin.VideoProcessor
{
	public class Camshift
	{
		private DenseHistogram histogram;

		private Image<Gray, byte> backProjection;

		private Rectangle trackWindow;
		private MCvConnectedComp trackComp;
		private MCvBox2D trackBox;

		private Hsv maskLower = new Hsv(0, 0, 200);
		private Hsv maskHigher = new Hsv(360, 360, 360);
		private Size frameSize;

		private bool showMask;
		private bool showHelp;

		public void CalculateHistogram(Image<Bgr, byte> frame, Rectangle rect)
		{
			histogram = new DenseHistogram(16, new RangeF(0, 180));
			trackWindow = rect;

			frameSize = frame.Size;
			backProjection = new Image<Gray, byte>(frameSize);

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
			var displayFrame = frame.Copy();

			if (histogram == null) {
				displayFrame.Draw("Not tracking", ref VisionExperiments.Font, new Point(30, 20), new Bgr(Color.Yellow));
				DrawHelp(displayFrame);
				return displayFrame;
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

			if (trackWindow.Width == 0) trackWindow.Width = RobinVideoConstants480P.MinBallEdgeLength;
			if (trackWindow.Height == 0) trackWindow.Height = RobinVideoConstants480P.MinBallEdgeLength;

			CvInvoke.cvCamShift(backProjection.Ptr, trackWindow, new MCvTermCriteria(5, 0.1), out trackComp, out trackBox);
			trackWindow = trackComp.rect;

			var ballCenter = trackWindow.Center();
			var maxEdgeLength = RobinVideoConstants480P.GetMaxBallEdgeLengthByFrameY(ballCenter.Y);
			if (trackWindow.Width > maxEdgeLength) trackWindow.Width = maxEdgeLength;
			if (trackWindow.Height > maxEdgeLength) trackWindow.Height = maxEdgeLength;

			if (showMask)
				displayFrame = mask.Convert<Bgr, byte>();
			//frame = backProjection.Convert<Bgr, byte>();

			//frame.Draw(rectangle, new Bgr(Color.DarkCyan), 2);
			var searchWindowString = string.Format("Search window max size: {0}", maxEdgeLength);

			displayFrame.Draw("Tracking", ref VisionExperiments.Font, new Point(60, 50), new Bgr(Color.Yellow));
			displayFrame.Draw(trackWindow, new Bgr(Color.Red), 3);
			displayFrame.Draw(searchWindowString, ref VisionExperiments.Font, new Point(60, 100), new Bgr(Color.Fuchsia));

			DrawHelp(displayFrame);

			return displayFrame;
		}

		private void DrawHelp(Image<Bgr, byte> frame)
		{
			var help = showHelp ?
				new[]
				{
					"H - hide help",
					"M - show mask",
					"P - pause/play",
					"Drag & Drop to set tracking window"
				}
				:
				new[] {"H - show help"};

			for (var i = 0; i < help.Length; i++)
			{
				var str = help[i];
				frame.Draw(str, ref VisionExperiments.Font, new Point(30, (frame.Height - 30) - (30 * i)), new Bgr(Color.Turquoise));
			}
		}

		public void SendKeyCommand(char key)
		{
			switch (char.ToLower(key))
			{
				case 'm':
					showMask = !showMask;
					break;
				case 'h':
					showHelp = !showHelp;
					break;
			}
		}
	}

	public static class DrawingExtensions
	{
		public static Point Center(this Rectangle rect)
		{
			return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
		}
	}

	public static class RobinVideoConstants480P
	{
		public const int MinBallEdgeLength = 10;

		private static readonly SortedDictionary<int, int> BallSizes =
			new SortedDictionary<int, int>
				{
					{ 100, 25 },
					{ 200, 40 },
					{ 300, 60 },
					{ 400, 80 },
					{ 500, 100 }
				};

		public static int GetMaxBallEdgeLengthByFrameY(int y)
		{
			return BallSizes.First(s => s.Key >= y).Value;
		}
	}
}