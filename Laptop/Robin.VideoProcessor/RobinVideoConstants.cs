using System;
using System.Drawing;

namespace Robin.VideoProcessor
{
	public static class RobinVideoConstants
	{
		public static readonly Func<int, int, int> RadiusFunc = (x, y) => (int)(36.9633 - (0.0714 * (480 - y)));
		public static readonly Size Size = new Size(640, 480);
	}
}