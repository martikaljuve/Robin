using System.Drawing;
using AForge.Imaging;

namespace Robin.VideoProcessor
{
	public static class GeometryExtensions
	{
		public static Point Center(this Rectangle rect)
		{
			return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
		}

		public static Rectangle GetRectangle(this HoughCircle circle)
		{
			return new Rectangle(circle.X - circle.Radius, circle.Y - circle.Radius, circle.Radius*2, circle.Radius*2);
		}
	}
}