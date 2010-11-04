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

		public static Image<Bgr, byte> HoughCircles(Bitmap source)
		{
			/*var colorFilter = new ExtractChannel(RGB.B);
			var blue = colorFilter.Apply(source);

			var filter = new Threshold(230);
			filter.ApplyInPlace(blue);

			//return blue;

			var cannyDetector = new CannyEdgeDetector();
			cannyDetector.ApplyInPlace(blue);*/
			/*
			Bitmap b = new Bitmap((int)f.Width, (int)f.Height);
			Graphics g = Graphics.FromImage(b);
			g.DrawImage(pictureBox1.Image,
		   new Rectangle(0, 0, (int)f.Width, (int)f.Height), (float)f.Left, (float)f.Top, (float)f.Width, (float)f.Height, GraphicsUnit.Pixel);

			_i2.ROI = new Rectangle<double>(f.Left, f.Right, f.Top, f.Bottom);
			c = _i2.Clone();

			pictureBox2.Image = c.ToBitmap();*/

			var blueFrame = new Image<Bgr, byte>(source)[0].PyrDown().PyrUp();
			
			//return blueFrame.Convert<Bgr, byte>();

			var canny = blueFrame.Canny(new Gray(230), new Gray(170));

			//return canny.Convert<Bgr, byte>();

			var circleList = new List<HoughCircle>();

			//var subGraphics = Graphics.FromImage(sub);
			//subGraphics.DrawImage(canny.Bitmap, 0, 0, new Rectangle(0, currentY, canny.Width, heights[i]), GraphicsUnit.Pixel);

			//Func<int, int, int> radiusFunc = (x, y) => (int) Math.Pow(y, 0.55);
			Func<int, int, int> radiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480-y)));
			var circleTransform = new HoughCircleTransformation(radiusFunc);
			circleTransform.ProcessImage(canny.Bitmap);
			var houghCircleImage = circleTransform.ToBitmap();
			//circleTransform.MaxIntensity;//0..255
			//100 / 255

			const int minIntensity = 19;

			var circles = circleTransform.GetCirclesByRelativeIntensity((double) minIntensity/circleTransform.MaxIntensity);
			//var circles = circleTransform.GetMostIntensiveCircles(9);
			circleList.AddRange(circles);

			//return new Image<Bgr, byte>(houghCircleImage);

			canny.ROI = Rectangle.Empty;
			
			var result = new Image<Gray, byte>(houghCircleImage);
			//var result = canny;
			//foreach (var circle in circleList)
			//	result.Draw(new CircleF(new PointF(circle.X, circle.Y), circle.Radius), new Gray(170), 2);

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