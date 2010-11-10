using System.Drawing;
using System.Runtime.InteropServices;

namespace Robin.ControlPanel
{
	public class GlobalMouse
	{
		[DllImport("user32.dll")]
		static extern bool GetCursorPos(ref Point lpPoint);

		private static Point lastPosition;

		static GlobalMouse()
		{
			GetPosition();
		}

		public static Point GetPosition()
		{
			GetCursorPos(ref lastPosition);
			return lastPosition;
		}

		public static Size GetRelativeMovement()
		{
			var oldPosition = lastPosition;
			GetPosition();

			return new Size(oldPosition.X - lastPosition.X, oldPosition.Y - lastPosition.Y);
		}
	}
}