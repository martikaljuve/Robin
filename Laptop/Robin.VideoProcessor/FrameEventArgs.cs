using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Robin.VideoProcessor
{
	public class FrameEventArgs : EventArgs
	{
		public FrameEventArgs(Image<Bgr, byte> frame)
		{
			Frame = frame;
		}

		public Image<Bgr, byte> Frame { get; set; }
	}
}