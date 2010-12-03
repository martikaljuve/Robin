using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Linq;

namespace Robin.VideoProcessor
{
	public static class HoughTransform
	{
		public static readonly HoughCircleTransformation CircleTransformation;

		private const double RhoResolution = 3;
		private const double ThetaResolution = (1 * Math.PI) / 180;
		private const int Threshold = 100;
		private const int MinLineWidth = 1;
		private const int GapBetweenLines = 40;

		static HoughTransform()
		{
			CircleTransformation = new HoughCircleTransformation(RobinVideoConstants.RadiusFunc);
			CircleTransformation.MinIntensityFunc = RobinVideoConstants.MinIntensityFunc;
			CircleTransformation.LocalPeakRadius = 1;
		}

		public static Image<Gray, byte> GetCanny(Image<Gray, byte> source)
		{
			return GetCanny(source, VideoParameters.Default.CannyThreshold, VideoParameters.Default.CannyThresholdLinking);
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
		/*
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
		}*/
	}
}