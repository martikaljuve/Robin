using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV.Structure;
using Robin.Core;

namespace Robin.VideoProcessor
{
	public class VisionResults
	{
		public IEnumerable<HoughCircle> Circles { get; set; }

		public bool TrackingBall { get; set; }

		public Rectangle TrackWindow { get; set; }

		public Point TrackCenter { get; set; }

		public IEnumerable<LineSegment2D> Lines { get; set; }

		public VisionData ToVisionData()
		{
			var data = new VisionData();
			data.TrackingBall = TrackingBall;
			data.TrackedBallLocation = TrackWindow.Center();
			data.FrontBallPathObstructed = false; // TODO: implement!
			data.OpponentGoalInFront = false;
			return data;
		}
	}
}