using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Image = AForge.Imaging.Image;
using System.Linq;

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
		private static Gray cannyThreshold = new Gray(150);
		private static Gray accumulatorThreshold = new Gray(100);
		private static bool toggleFrame;
		private static int channel;

		public static Image<Bgr, byte> CannyEdges(Image<Bgr, byte> frame)
		{
			Image<Bgr, byte> result;

			if (CheckKey('u'))
				min += 10;
			if (CheckKey('j'))
				min -= 10;

			if (CheckKey('i'))
				cannyThreshold.Intensity += 10;
			if (CheckKey('k'))
				cannyThreshold.Intensity -= 10;

			if (CheckKey('o'))
				accumulatorThreshold.Intensity += 10;
			if (CheckKey('l'))
				accumulatorThreshold.Intensity -= 10;

			if (CheckKey('t')) toggleFrame = !toggleFrame;
			
			result = toggleFrame ? frame[0].ThresholdBinary(new Gray(240), new Gray(360)).Convert<Bgr, byte>() : frame.Copy();

			using (var gray = result[0].ThresholdBinary(new Gray(240), new Gray(360)).PyrDown().PyrUp())
			{
				//result = gray.Canny(cannyThreshold, accumulatorThreshold).Convert<Bgr, byte>();
				result.Draw("Accumulator: " + accumulatorThreshold.Intensity, ref Font, new Point(10, 30), new Bgr(Color.Green));
				result.Draw("Canny: " + cannyThreshold.Intensity, ref Font, new Point(10, 60), new Bgr(Color.Green));

				var circles = gray.HoughCircles(
					cannyThreshold,
					accumulatorThreshold,
					2.0,
					10.0,
					3,
					30)[0];

				foreach (var circle in circles)
					result.Draw(circle, new Bgr(Color.DeepPink), 2);
			}

			return result.Convert<Bgr, byte>();
			
		}

		public static int Threshold = 300;
		public static int ThresholdLinking = 500;
		public static IEnumerable<HoughCircle> Circles;
		public static Bitmap FindGolfBallsWithHoughAndColorFilter(Bitmap source)
		{
			// HACK: Testing
			if (CheckKey('1'))
				channel = 1;
			if (CheckKey('2'))
				channel = 2;
			if (CheckKey('3'))
				channel = 3;
			if (CheckKey('4'))
				channel = 4;

			if (CheckKey('u'))
				Threshold += 100;
			if (CheckKey('j'))
				Threshold -= 100;

			if (CheckKey('i'))
				ThresholdLinking += 100;
			if (CheckKey('k'))
				ThresholdLinking -= 100;
			// HACK: END

			//return source;

			var display = new Image<Bgr, byte>(source);
			var blue = display[0];//.ThresholdBinary(new Gray(220), new Gray(255));

			// HACK: Testing
			//return blue.Convert<Bgr, byte>().Bitmap;

			var canny = blue.Canny(new Gray(Threshold), new Gray(ThresholdLinking));

			Func<int, int, int> radiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480-y)));
			var houghTransformation = new HoughCircleTransformation(radiusFunc);

			houghTransformation.MinIntensityFunc =
				(x, y) => { return (int)(2.64636615605 + 0.0474890367351 * y); };

			houghTransformation.ProcessImage(canny.Bitmap);

			Circles = houghTransformation.GetMostIntensiveCircles(9);

			// HACK: Testing));)
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
					return new Image<Bgr, byte>(houghTransformation.ToBitmap()).Bitmap;
			}
			
			// HACK: Testing
			//return canny.Convert<Bgr, byte>().Bitmap;

			return display.Bitmap;
		}

		public static Image<Bgr, byte> HoughCircles(Bitmap source)
		{
			//var hls = new Image<Hls, byte>(source);
			//var blueFrame = hls[1].PyrDown().PyrUp();

			var blueFrame = new Image<Bgr, byte>(source)[2].PyrDown().PyrUp();
			
			//return blueFrame.Convert<Bgr, byte>();

			var canny = blueFrame.Canny(new Gray(230), new Gray(170));

			//return canny.Convert<Bgr, byte>();

			var circleList = new List<HoughCircle>();

			//var subGraphics = Graphics.FromImage(sub);
			//subGraphics.DrawImage(canny.Bitmap, 0, 0, new Rectangle(0, currentY, canny.Width, heights[i]), GraphicsUnit.Pixel);

			//Func<int, int, int> radiusFunc = (x, y) => (int) Math.Pow(y, 0.55);
			Func<int, int, int> radiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480-y)));
			var circleTransform = new HoughCircleTransformation(radiusFunc);
			circleTransform.MinIntensityFunc =
				(x, y) =>
					{
						//e^(1.40733703025 + 0.0036315044155 * x)
						return (int)(2.64636615605 + 0.0474890367351 * y);
					};
			circleTransform.ProcessImage(canny.Bitmap);
			var houghCircleImage = circleTransform.ToBitmap();
			//circleTransform.MaxIntensity;//0..255
			//100 / 255

			const int minIntensity = 20;

			//var circles = circleTransform.GetCirclesByRelativeIntensity((double) minIntensity/circleTransform.MaxIntensity);
			var circles = circleTransform.GetMostIntensiveCircles(9);
			circles = circles.Where(x => x.Intensity > (minIntensity / circleTransform.MaxIntensity)).ToArray();

			circleList.AddRange(circles);

			//return new Image<Bgr, byte>(houghCircleImage);

			canny.ROI = Rectangle.Empty;

			var result = new Image<Bgr, byte>(source);
			//var result = new Image<Gray, byte>(houghCircleImage);
			//var result = canny;
			foreach (var circle in circleList)
			{
				/*hls.ROI = Rectangle.Empty;

				var left = Math.Max(0, circle.X - circle.Radius);
				var right = Math.Min(hls.Width, circle.X + circle.Radius);
				var top = Math.Max(0, circle.Y - circle.Radius);
				var bottom = Math.Min(hls.Height, circle.Y + circle.Radius);
				
				hls.ROI = new Rectangle(
					left,
					top,
					(int) Math.Min(10, (right - left) * 0.66),
					(int) Math.Min(10, (bottom - top) * 0.66)
				);

				var avg = hls.GetAverage();
				if (avg.Lightness < 200) continue;*/

				//var distance = -0.6405 + (81.275 * Math.Log(480 - circle.Y));
				var distance = Math.Pow(Math.E, 0.0272 + 0.01225 * (480 - circle.Y));
				result.Draw(new CircleF(new PointF(circle.X, circle.Y), circle.Radius), new Bgr(Color.Yellow), 2);
				result.Draw(distance.ToString("0.00"), ref Font, new Point(circle.X, circle.Y), new Bgr(Color.Orange));
			}

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