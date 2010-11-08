using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
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