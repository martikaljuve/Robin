using System.Collections.Generic;
using System.Drawing;
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

				/*var sum = tempRect.GetSum();
				sum.Hue /= 8100;
				sum.Lightness /= 8100;
				sum.Satuation /= 8100;
				*/

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

		public static Image<Bgr, byte> FilterByColor(Image<Bgr, byte> frame)
		{
			var hls = frame.Convert<Hls, byte>();
			var result = hls.InRange(new Hls(0.0, 200.0, 0.0), new Hls(360.0, 360.0, 360.0));

			return result.Convert<Bgr, byte>();

			//return frame;
		}

		private static double min = 150;
		public static Image<Bgr, byte> CannyEdges(Image<Bgr, byte> frame)
		{
			Image<Bgr, byte> result;

			if (CheckKey('u'))
				min += 10;
			if (CheckKey('j'))
				min -= 10;

			if (CheckKey('t'))
				result = frame;
			else
			{
				result = frame.ThresholdBinary(new Bgr(min, min, min), new Bgr(255, 255, 255));
			}

			var gray = result.Convert<Gray, byte>();
			//var circles = gray.HoughCircles(new Gray(210), new Gray(255), 5.0, 50.0, 1, 50)[0];
			result = gray.Convert<Bgr, byte>();
			//foreach (var circle in circles)
				//result.Draw(circle, new Bgr(Color.DeepPink), 2);

			//var result = hsv.Canny(new Gray(100), new Gray(360));))
			return result.Convert<Bgr, byte>();
		}

		public static void ProcessKey(char key)
		{
			if (!Keys.ContainsKey(key))
				Keys.Add(key, false);

			Keys[key] = !Keys[key];
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