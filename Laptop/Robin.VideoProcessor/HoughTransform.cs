using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public static class HoughTransform
	{
		public static readonly HoughCircleTransformation CircleTransformation;
		private const int CannyThreshold = 100;
		private const int CannyThresholdLinking = 100;

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

		public static Image<Gray, byte> GetCanny(
			Image<Gray, byte> source,
			double threshold = CannyThreshold,
			double thresholdLinking = CannyThresholdLinking)
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
	}
}