using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV.Structure;
using Robin.Core;
using System.Linq;

namespace Robin.VideoProcessor
{
	public class VisionResults
	{
		public IEnumerable<HoughCircle> Circles { get; set; }

		public bool TrackingBall { get; set; }

		public Rectangle TrackWindow { get; set; }

		public Point TrackCenter { get; set; }

		public IEnumerable<LineSegment2D> Lines { get; set; }

		public IEnumerable<Rectangle> GoalRectangles { get; set; }

		public VisionData ToVisionData()
		{
			var data = new VisionData();

			data.TrackingBall = TrackingBall;
			data.TrackedBallLocation = TrackWindow.Center();
			data.FrontBallPathObstructed = false; // TODO: implement!

			if (GoalRectangles != null)
			{
				var bestGoal = GoalRectangles.OrderByDescending(x => x.Width).FirstOrDefault();
				data.OpponentGoalRectangle = bestGoal;
				if (bestGoal == Rectangle.Empty)
					data.OpponentGoalOffset = null;
				else
					data.OpponentGoalOffset = (short) ((VisionData.FrameSize.Width / 2) - bestGoal.Center().X);
			}
			return data;
		}
	}
}