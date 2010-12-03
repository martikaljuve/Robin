using System.Drawing;

namespace Robin.Core
{
	public class VisionData
	{
		public bool FrontBallPathObstructed { get; set; }
		public bool TrackingBall { get; set; }
		public Point TrackedBallLocation { get; set; }
		public bool OpponentGoalInFront { get; set; }
		public int OpponentGoalOffset { get; set; }

		public static readonly Size FrameSize = new Size(640, 480);
	}
}