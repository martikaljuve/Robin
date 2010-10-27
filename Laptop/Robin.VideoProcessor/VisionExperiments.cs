using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VisionExperiments
	{
		public static MCvFont Font;

		static VisionExperiments()
		{
			Font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX_SMALL, 1.0, 1.0);
		}

		public static Image<Bgr, byte> GetAverageForSubRectangle(Image<Bgr, byte> frame)
		{
			Image<Hls, byte> fr;
			using (var tempFrame = frame.Convert<Hls, byte>())
			using (var tempRect = tempFrame.GetSubRect(new Rectangle(295, 90, 90, 90)))
			using (var tempRect2 = new Image<Hls, byte>(270, 90))
			{
				fr = new Image<Hls, byte>(360, 90);

				fr.ROI = new Rectangle(0, 0, 90, 90);
				CvInvoke.cvAddWeighted(tempRect.Ptr, 1.0, fr.Ptr, 0.0, 0, fr.Ptr);

				/*var sum = tempRect.GetSum();
				sum.Hue /= 8100;
				sum.Lightness /= 8100;
				sum.Satuation /= 8100;
				*/

				var sum = tempRect.GetAverage();

				tempRect2.Draw(new Rectangle(0, 0, 90, 90), sum, 0);
				tempRect2.Draw(new Rectangle(90, 0, 90, 90), new Hls(0, sum.Satuation, 1), 0);
				tempRect2.Draw(new Rectangle(180, 0, 90, 90), new Hls(0, sum.Lightness, 1), 0);

				tempRect2.Draw("H" + sum.Hue.ToString("0.0"), ref Font, new Point(0, 25), new Hls(0, 0, 0));
				tempRect2.Draw("S" + sum.Satuation.ToString("0.0"), ref Font, new Point(180, 75), new Hls(0, 0, 0));
				tempRect2.Draw("L" + sum.Lightness.ToString("0.0"), ref Font, new Point(90, 50), new Hls(0, 0, 0));

				fr.ROI = new Rectangle(90, 0, 270, 90);
				CvInvoke.cvAddWeighted(tempRect2.Ptr, 1.0, fr.Ptr, 0.0, 0, fr.Ptr);
			}

			fr.ROI = Rectangle.Empty;

			return fr.Convert<Bgr, byte>();
		}

		public static Image<Bgr, byte> FilterByColor(Image<Bgr, byte> frame)
		{
			var hls = frame.Convert<Hls, byte>();
			var result = hls.InRange(new Hls(0.0, 200.0, 0.0), new Hls(360.0, 360.0, 360.0));

			return result.Convert<Bgr, byte>();

			//return frame;
		}
	}
}