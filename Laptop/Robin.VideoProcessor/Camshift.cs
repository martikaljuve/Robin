using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class Camshift
	{
		private DenseHistogram histogram;

		private Image<Gray, byte> backProjection;
		private Image<Gray, byte> mask;

		private Rectangle trackWindow;
		private PointF trackCenter;
		private MCvConnectedComp trackComp;

		private readonly Gray maskLow = new Gray(100);
		private readonly Gray maskHigh = new Gray(360);
		
		public Camshift()
		{
			histogram = new DenseHistogram(16, new RangeF(0, 180));
		}
		
		public void CalculateHistogram(Image<Gray, byte> source)
		{
			histogram = new DenseHistogram(16, new RangeF(0, 180));

			mask = source.InRange(maskLow, maskHigh);
			CvInvoke.cvCalcHist(new[] { source.Ptr }, histogram.Ptr, false, mask.Ptr);

			SetTrackWindow(source.ROI);
		}

		public void SetTrackWindow(Rectangle rectangle)
		{
			trackWindow = rectangle;
		}

		public void Track(Image<Gray, byte> source)
		{
			if (histogram == null)
				return;

			mask = source.InRange(maskLow, maskHigh);
			backProjection = new Image<Gray, byte>(source.Size);

			CvInvoke.cvCalcBackProject(new[] {source.Ptr}, backProjection.Ptr, histogram.Ptr);
			backProjection._And(mask);

			MCvBox2D trackBox;
			CvInvoke.cvCamShift(backProjection.Ptr, trackWindow, new MCvTermCriteria(10, 1), out trackComp, out trackBox);
			
			trackWindow = trackComp.rect;
			trackCenter = trackBox.center;
		}

		public Rectangle TrackWindow
		{
			get { return trackWindow; }
		}

		public Point TrackCenter
		{
			get { return Point.Truncate(trackCenter); }
		}

		public Image<Gray, byte> Mask
		{
			get { return mask; }
		}

		public Image<Gray, byte> BackProjection
		{
			get { return backProjection; }
		}
	}
}