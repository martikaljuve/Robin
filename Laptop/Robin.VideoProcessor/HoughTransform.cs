using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Linq;
using Image = AForge.Imaging.Image;

namespace Robin.VideoProcessor
{
	public static class HoughTransform
	{
		public static readonly HoughCircleTransformation CircleTransformation;
		public static int CannyThreshold = 100;
		public static int CannyThresholdLinking = 100;

		private const double RhoResolution = 1;
		private const double ThetaResolution = Math.PI / 90;
		private const int Threshold = 30;
		private const int MinLineWidth = 5;
		private const int GapBetweenLines = 10;

		private static readonly Func<int, int, int> RadiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480 - y)));
		private static readonly Func<int, int, int> MinIntensityFunc =
			(x, y) =>
				{
					if (y < 50) return 15;
					if (y < 100) return 20;
					if (y < 150) return 20;
					if (y < 200) return 25;
					if (y < 250) return 30;
					if (y < 300) return 35;
					if (y < 350) return 40;
					return 50;
				};
		//private static readonly Func<int, int, int> MinIntensityFunc = (x, y) => (int)(2.64636615605 + 0.0474890367351 * y);

		static HoughTransform()
		{
			CircleTransformation = new HoughCircleTransformation(RadiusFunc);
			CircleTransformation.MinIntensityFunc = MinIntensityFunc;
		}

		public static Image<Gray, byte> GetCanny(Image<Gray, byte> source)
		{
			return GetCanny(source, CannyThreshold, CannyThresholdLinking);
		}

		private static Image<Gray, byte> GetCanny(Image<Gray, byte> source, double threshold, double thresholdLinking)
		{
			return source.Canny(new Gray(threshold), new Gray(thresholdLinking));
		}

		public static IEnumerable<LineSegment2D> GetLines(Image<Gray, byte> canny)
		{
			var lines = canny.HoughLinesBinary(RhoResolution, ThetaResolution, Threshold, MinLineWidth, GapBetweenLines)[0];

			return lines;
		}

		public static IEnumerable<HoughCircle> GetCircles(Bitmap canny)
		{
			CircleTransformation.ProcessImage(canny);

			return CircleTransformation.Circles;
		}

		public static IEnumerable<LineSegment2D> FilterLines(IEnumerable<LineSegment2D> lines)
		{
			Func<Point, bool> onEdge = p => p.X < 5 || p.Y < 5 || p.X > 635 || p.Y > 475;
			return lines.Where(
				x =>
				{
					var tmp1 = x.Length > 20;
					var tmp2 = onEdge(x.P1);
					var tmp3 = onEdge(x.P2);
					return tmp1 || tmp2 || tmp3;
				});
		}
	}

	public static class EdgeFinder
	{
		public static List<Point> GetTopArea(Image<Gray, byte> blue, IEnumerable<LineSegment2D> lines)
		{
			Func<Point, bool> isTopWallEdge =
				point =>
				{
					var sumUp = 0;
					for (var y = point.Y; y < Math.Max(0, point.Y - 5); y++)
						sumUp += blue.Data[y, point.X, 0];

					var sumDown = 0;
					for (var y = point.Y; y <= Math.Min(blue.Height, point.Y + 5); y++)
						sumDown += blue.Data[y, point.X, 0];

					return sumUp > 500 && sumDown < 500;
				};

			var points = new List<Point>();

			foreach (var line in lines)
			{
				if (line.Direction.X < 0.2 && line.Direction.X > -0.2)
					continue;
				if (isTopWallEdge(line.P1))
					points.Add(line.P1);
				if (isTopWallEdge(line.P2))
					points.Add(line.P2);
			}
			points.Sort((p1, p2) => Comparer<int>.Default.Compare(p1.X, p2.X));
			points.Add(new Point(640, 0));
			points.Add(new Point(0, 0));

			return points;
		}

		public static IEnumerable<Contour<Point>> GetContours(Image<Gray, byte> canny)
		{
			using (var storage = new MemStorage())
			{
				var contours = canny.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, RETR_TYPE.CV_RETR_TREE, storage);

				while (contours != null)
				{
					yield return contours;
					contours = contours.HNext;
				}
			}
		}
	}
}