using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VisionResults
	{
		public IEnumerable<HoughCircle> Circles;
		public bool TrackingBall;
		public Rectangle TrackWindow;
		public IEnumerable<LineSegment2D> Lines;
	}
}