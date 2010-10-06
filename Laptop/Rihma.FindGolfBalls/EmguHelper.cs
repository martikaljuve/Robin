using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Rihma.FindGolfBalls
{
	public static class EmguHelper
	{
		public static MCvFont NormalFont = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
		public static MCvFont SmallFont = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);
	}
}