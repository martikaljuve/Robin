using System;

namespace Robin.VideoProcessor
{
	public static class RobinVideoConstants
	{
		public static readonly Func<int, int, int> RadiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480 - y)));

		public static readonly Func<int, int, int> MinIntensityFunc =
			(x, y) =>
			{
				if (y < 50) return 15 - 3;
				if (y < 100) return 20 - 3;
				if (y < 150) return 20 - 3;
				if (y < 200) return 35 - 6;
				if (y < 250) return 40 - 13;
				if (y < 300) return 45 - 16;
				if (y < 350) return 50 - 16;
				return 50 - 13;
			};

		//public static readonly Func<int, int, int> MinIntensityFunc = (x, y) => (int)(2.64636615605 + 0.0474890367351 * y);
	}
}