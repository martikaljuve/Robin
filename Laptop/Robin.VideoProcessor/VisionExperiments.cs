using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public static class VisionExperiments
	{
		public static MCvFont Font;
		private static readonly Dictionary<char,bool> Keys = new Dictionary<char, bool>();

		static VisionExperiments()
		{
			Font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);
		}

		public static Image<Bgr, byte> GetAverageForSubRectangle(Image<Bgr, byte> frame)
		{
			Image<Hls, byte> fr;
			using (var tempFrame = frame.Convert<Hls, byte>())
			using (var tempRect = tempFrame.GetSubRect(new Rectangle(295, 90, 90, 90)))
			using (var tempRect2 = new Image<Hls, byte>(270, 90))
			{
				fr = new Image<Hls, byte>(360, 90);

				fr.ROI = new Rectangle(0, 0, 90, 90);
				CvInvoke.cvAddWeighted(tempRect.Ptr, 1.0, fr.Ptr, 0.0, 0, fr.Ptr);
				
				var sum = tempRect.GetAverage();

				tempRect2.Draw(new Rectangle(0, 0, 90, 90), sum, 0);
				tempRect2.Draw(new Rectangle(90, 0, 90, 90), new Hls(0, sum.Satuation, 1), 0);
				tempRect2.Draw(new Rectangle(180, 0, 90, 90), new Hls(0, sum.Lightness, 1), 0);

				tempRect2.Draw("H" + sum.Hue.ToString("0.0"), ref Font, new Point(0, 25), new Hls(0, 0, 0));
				tempRect2.Draw("S" + sum.Satuation.ToString("0.0"), ref Font, new Point(180, 75), new Hls(0, 0, 0));
				tempRect2.Draw("L" + sum.Lightness.ToString("0.0"), ref Font, new Point(90, 50), new Hls(0, 0, 0));

				fr.ROI = new Rectangle(90, 0, 270, 90);
				CvInvoke.cvAddWeighted(tempRect2.Ptr, 1.0, fr.Ptr, 0.0, 0, fr.Ptr);
			}

			fr.ROI = Rectangle.Empty;

			return fr.Convert<Bgr, byte>();
		}
		
		private static int channel;
		public static IEnumerable<HoughCircle> Circles;
		public static IEnumerable<LineSegment2D> Lines;
		private static readonly Image<Gray, byte> RobotMask = new Image<Gray, byte>(@"Resources\RobotMask.bmp");

		public static Bitmap FindCirclesAndLinesWithHough(Bitmap source)
		{
			var display = new Image<Bgr, byte>(source);

			var blue = display[0]
				.PyrDown()
				.Dilate(2)
				.Erode(2)
				.ThresholdBinary(new Gray(130), new Gray(500))
				.PyrUp();
			
			var canny = HoughTransform.GetCanny(blue);
			canny.SetValue(new Gray(0), RobotMask);

			var lines = HoughTransform.GetLines(canny);
			//Lines = lines;
			Lines = HoughTransform.FilterLines(lines);
			Circles = HoughTransform.GetCircles(canny.Bitmap);

			//var points = EdgeFinder.GetTopArea(blue, lines);
			//canny.FillConvexPoly(points.ToArray(), new Gray(155));

			//var contours = EdgeFinder.GetContours(canny);
			//foreach (var contour in contours)
			//	canny.FillConvexPoly(contour.ToArray(), new Gray(150));

			// HACK: Testing)
			switch (channel)
			{
				default:
				case 1:
					return display.Bitmap;
				case 2:
					return blue.Convert<Bgr, byte>().Bitmap;
				case 3:
					return canny.Convert<Bgr, byte>().Bitmap;
				case 4:
					return new Image<Bgr, byte>(HoughTransform.CircleTransformation.ToBitmap()).Bitmap;
				case 5:
					return display.CopyBlank().Bitmap;
			}
		}

		private static void ToggleKeys()
		{
			if (CheckKey('1'))
				channel = 1;
			if (CheckKey('2'))
				channel = 2;
			if (CheckKey('3'))
				channel = 3;
			if (CheckKey('4'))
				channel = 4;
			if (CheckKey('5'))
				channel = 5;

			if (CheckKey('u'))
				HoughTransform.CannyThreshold += 10;
			if (CheckKey('j'))
				HoughTransform.CannyThreshold -= 10;

			if (CheckKey('i'))
				HoughTransform.CannyThresholdLinking += 10;
			if (CheckKey('k'))
				HoughTransform.CannyThresholdLinking -= 10;
		}

		public static void ProcessKey(char key)
		{
			if (!Keys.ContainsKey(key))
				Keys.Add(key, false);

			Keys[key] = !Keys[key];

			ToggleKeys();
		}

		private static bool CheckKey(char key)
		{
			if (!Keys.ContainsKey(key))
				Keys.Add(key, false);

			if (Keys[key]) {
				Keys[key] = false;
				return true;
			}

			return false;
		}
	}
}