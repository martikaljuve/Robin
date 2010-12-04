using System.Drawing;

namespace Robin.Core
{
	public class VisionData
	{
		public bool FrontBallPathObstructed { get; set; }
		public bool TrackingBall { get; set; }
		public Point TrackedBallLocation { get; set; }
		
		public short? OpponentGoalOffset { get; set; }

		public bool OpponentGoalInFront
		{
			get
			{
				return OpponentGoalRectangle != Rectangle.Empty &&
					(OpponentGoalRectangle.Left < FrameSize.Width / 2) &&
					(OpponentGoalRectangle.Right > FrameSize.Width / 2);
			}
		}

		public Rectangle OpponentGoalRectangle { get; set; }

		public static readonly Size FrameSize = new Size(640, 480);
	}
}