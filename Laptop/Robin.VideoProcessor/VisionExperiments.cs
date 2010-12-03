using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Robin.Core;
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
		
		private static int channel;
		public static IEnumerable<HoughCircle> Circles;
		public static IEnumerable<LineSegment2D> Lines;
		public static IEnumerable<Rectangle> GoalRectangles;
		private static readonly Image<Gray, byte> RobotMask = new Image<Gray, byte>(@"Resources\RobotMask.bmp");

		private static Image<Bgr, byte> frameBgr;
		private static Image<Gray, byte> frameGray;
		private static Image<Gray, byte> frameCanny;

		public static Bitmap FindCirclesAndLinesWithHough(Bitmap source)
		{
			frameBgr = new Image<Bgr, byte>(source);

			frameGray = frameBgr[0]
				.PyrDown()
				.Dilate(2)
				.Erode(2)
				.ThresholdBinary(VideoParameters.Default.GrayFrameThresholdLow, VideoParameters.Default.GrayFrameThresholdHigh)
				.PyrUp();
			
			frameCanny = HoughTransform.GetCanny(frameGray);

			var blackGray = new Gray(0);
			var blackBgr = new Bgr(Color.Black);
			frameCanny.SetValue(blackGray, RobotMask);

			var lines = HoughTransform.GetLines(frameCanny);
			Lines = lines;

			var height = VisionData.FrameSize.Height;
			foreach (var line in lines)
			{
				if (line.Length < 10 /*&& IsLineWhite(frameBgr, line)*/)
					continue;

				var polygon = new[]
				{
				    new Point(line.P1.X, line.P1.Y + 5),
				    new Point(line.P1.X, 0),
				    new Point(line.P2.X, 0),
				    new Point(line.P2.X, line.P2.Y + 5)
				};
				/*
				var newLine = GetEdgeLine(line.P1, line.P2);
				var polygon = new[]
				{
					new Point(newLine.P1.X, newLine.P1.Y),
					new Point(newLine.P1.X, newLine.P1.Y - height),
					new Point(newLine.P2.X, newLine.P2.Y - height),
					new Point(newLine.P2.X, newLine.P2.Y)
				};
				 */

				frameCanny.FillConvexPoly(polygon, blackGray);
				frameBgr.FillConvexPoly(polygon, blackBgr);
				//frameCanny.Draw(line, new Gray(0), 5);
			}

			//Lines = HoughTransform.FilterLines(lines);
			Circles = HoughTransform.GetCircles(frameCanny.Bitmap);

			//var points = EdgeFinder.GetTopArea(blue, lines);
			//canny.FillConvexPoly(points.ToArray(), new Gray(155));

			//var contours = EdgeFinder.GetContours(canny);
			//foreach (var contour in contours)
			//	canny.FillConvexPoly(contour.ToArray(), new Gray(150));

			// HACK: Testing))
			switch (channel)
			{
				default:
				case 1:
					return frameBgr.Bitmap;
				case 2:
					return frameGray.Convert<Bgr, byte>().Bitmap;
				case 3:
					return frameCanny.Convert<Bgr, byte>().Bitmap;
				case 4:
					return new Image<Bgr, byte>(HoughTransform.CircleTransformation.ToBitmap()).Bitmap;
				case 5:
					return frameBgr.CopyBlank().Bitmap;
				case 6:
					var frame = frameBgr.InRange(
						new Bgr(0, 0, 50),
						new Bgr(VideoParameters.Default.RedThresholdBlue, VideoParameters.Default.RedThresholdGreen, VideoParameters.Default.RedThresholdRed));
					return frame
						.Dilate(3).Erode(6).Dilate(3)
						.Convert<Bgr, byte>().Bitmap;
				case 7:
					var frame2 = frameBgr.InRange(
						new Bgr(50, 0, 0),
						new Bgr(VideoParameters.Default.BlueThresholdBlue, VideoParameters.Default.BlueThresholdGreen, VideoParameters.Default.BlueThresholdRed));
					return frame2
						.Dilate(3).Erode(6).Dilate(3)
						.Convert<Bgr, byte>().Bitmap;
				case 8:
					var rectanglesRed = FindRedGoalRectangles();
					var i = 1;
					foreach (var rectangle in rectanglesRed.OrderBy(x => x.Size.Width))
					{
						frameBgr.Draw(rectangle, new Bgr(Color.Red), 3);
						frameBgr.Draw(i.ToString(), ref Font, rectangle.Location + new Size(10, 10), new Bgr(Color.DarkRed));
					}

					var rectanglesBlue = FindBlueGoalRectangles();
					i = 1;
					foreach (var rectangle in rectanglesBlue.OrderBy(x => x.Size.Width))
					{
						frameBgr.Draw(rectangle, new Bgr(Color.Blue), 3);
						frameBgr.Draw(i.ToString(), ref Font, rectangle.Location + new Size(10, 10), new Bgr(Color.DarkBlue));
					}

					return frameBgr.Bitmap;
				case 9:
					return frameGray
						.InRange(VideoParameters.Default.CamshiftMaskLow, VideoParameters.Default.CamshiftMaskHigh)
						.Convert<Bgr, byte>()
						.Bitmap;
			}
		}

		private static LineSegment2D GetEdgeLine(Point p1, Point p2)
		{
			var x2 = VisionData.FrameSize.Width;
			var y1 = (((p2.Y - p1.Y) / (double)(p2.X - p1.X)) * (0 - p1.X)) + p1.Y;
			var y2 = (((p2.Y - p1.Y) / (double)(p2.X - p1.X)) * (x2 - p1.X)) + p1.Y;
			return new LineSegment2D(new Point(0, (int)y1), new Point(x2, (int)y2));
		}

		private static bool IsLineWhite(Image<Bgr, byte> image, LineSegment2D line)
		{
			var rect1 = new Rectangle(new Point(line.P1.X, line.P1.Y - 5), new Size(1, 5));
			var rect2 = new Rectangle(new Point(line.P2.X, line.P1.Y - 5), new Size(1, 5));

			image.ROI = rect1;
			var avg1 = image.GetSum();
			image.ROI = rect2;
			var avg2 = image.GetSum();
			image.ROI = Rectangle.Empty;

			const int threshold = 10 * 5;

			var line1 = avg1.Red > threshold && avg1.Green > threshold && avg1.Blue > threshold;
			var line2 = avg2.Red > threshold && avg2.Green > threshold && avg2.Blue > threshold;

			return line1 && line2;
		}

		public static IEnumerable<Rectangle> FindRedGoalRectangles()
		{
			var frame = frameBgr.InRange(
						new Bgr(0, 0, 50),
						new Bgr(VideoParameters.Default.RedThresholdBlue, VideoParameters.Default.RedThresholdGreen, VideoParameters.Default.RedThresholdRed));

			return FindGoalRectangles(frame);
		}

		public static IEnumerable<Rectangle> FindBlueGoalRectangles()
		{
			var frame = frameBgr.InRange(
						new Bgr(50, 0, 0),
						new Bgr(VideoParameters.Default.BlueThresholdBlue, VideoParameters.Default.BlueThresholdGreen, VideoParameters.Default.BlueThresholdRed));

			return FindGoalRectangles(frame);
		}

		private static IEnumerable<Rectangle> FindGoalRectangles(Image<Gray, byte> gray)
		{
			gray = gray.Dilate(3).Erode(6).Dilate(3);

			var contours = gray.Canny(new Gray(100), new Gray(100)).FindContours();

			var rectangles = new List<Rectangle>();
			if (contours != null)
				do
				{
					if (contours.BoundingRectangle.Width > 15)
						rectangles.Add(contours.BoundingRectangle);

					//frameBgr.Draw(contours.BoundingRectangle, new Bgr(Color.Firebrick), 1);
					//contours.ApproxPoly(contours.Perimeter*0.5, storage);
					//if (contours.Area < 50) continue;
					//frame.DrawPolyline(contours.ToArray(), true, new Gray(200), 1);
				} while ((contours = contours.HNext) != null);

			return rectangles;
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
			if (CheckKey('6'))
				channel = 6;
			if (CheckKey('7'))
				channel = 7;
			if (CheckKey('8'))
				channel = 8;
			if (CheckKey('9'))
				channel = 9;

			if (CheckKey('u'))
				VideoParameters.Default.CannyThreshold += 10;
			if (CheckKey('j'))
				VideoParameters.Default.CannyThreshold -= 10;

			if (CheckKey('i'))
				VideoParameters.Default.CannyThresholdLinking += 10;
			if (CheckKey('k'))
				VideoParameters.Default.CannyThresholdLinking -= 10;
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

		public static Image<Bgr, byte> FrameBgr
		{
			get { return frameBgr; }
		}

		public static Image<Gray, byte> FrameGray
		{
			get { return frameGray; }
		}

		public static Image<Gray, byte> FrameCanny
		{
			get { return frameCanny; }
		}
	}
}