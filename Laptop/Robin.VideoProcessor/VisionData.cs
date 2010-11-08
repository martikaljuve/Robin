using System;
using System.Drawing;

namespace Robin.VideoProcessor
{
	public class VisionData
	{
		public bool FrontBallPathObstructed { get; set; }
		public bool TrackingBall { get; set; }
		public Point TrackedBallLocation { get; set; }

		public void UpdateFromVisionResults(VisionResults visionResults)
		{
			TrackingBall = visionResults.TrackingBall;
			TrackedBallLocation = visionResults.TrackWindow.Center();
		}
	}
}