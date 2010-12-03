using System.ComponentModel;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class VideoParameters
	{
		private static readonly VideoParameters _default = new VideoParameters();

		public static VideoParameters Default
		{
			get { return _default; }
		}

		private VideoParameters()
		{
			GrayFrameThresholdLow = new Gray(150);
			GrayFrameThresholdHigh = new Gray(500);

			CamshiftMaskLow = new Gray(100);
			CamshiftMaskHigh = new Gray(360);

			CannyThreshold = 100;
			CannyThresholdLinking = 100;

			RedThresholdRed = 300;
			RedThresholdGreen = 35;
			RedThresholdBlue = 35;

			BlueThresholdBlue = 300;
			BlueThresholdGreen = 35;
			BlueThresholdRed = 35;
		}

		[Browsable(false)]
		public Gray GrayFrameThresholdLow { get; set; }

		[Category("Hough")]
		public double GrayFrameThresholdLowIntensity
		{
			get { return GrayFrameThresholdLow.Intensity; }
			set { GrayFrameThresholdLow = new Gray(value); }
		}

		[Browsable(false)]
		public Gray GrayFrameThresholdHigh { get; set; }

		[Category("Hough")]
		public double GrayFrameThresholdHighIntensity
		{
			get { return GrayFrameThresholdHigh.Intensity; }
			set { GrayFrameThresholdHigh = new Gray(value); }
		}

		[Browsable(false)]
		public Gray CamshiftMaskLow { get; set; }

		[Category("Camshift")]
		public double CamshiftMaskLowIntensity
		{
			get { return CamshiftMaskLow.Intensity; }
			set { CamshiftMaskLow = new Gray(value); }
		}

		[Browsable(false)]
		public Gray CamshiftMaskHigh { get; set; }

		[Category("Camshift")]
		public double CamshiftMaskHighIntensity
		{
			get { return CamshiftMaskHigh.Intensity; }
			set { CamshiftMaskHigh = new Gray(value); }
		}

		[Category("Hough")]
		public double CannyThreshold { get; set; }

		[Category("Hough")]
		public double CannyThresholdLinking { get; set; }

		public double RedThresholdRed { get; set; }
		public double RedThresholdGreen { get; set; }
		public double RedThresholdBlue { get; set; }

		public double BlueThresholdRed { get; set; }
		public double BlueThresholdGreen { get; set; }
		public double BlueThresholdBlue { get; set; }
	}
}