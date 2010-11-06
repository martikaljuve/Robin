using System;
using System.Drawing;

namespace Robin.VideoProcessor
{
	public class FrameEventArgs : EventArgs
	{
		public FrameEventArgs(Bitmap frame)
		{
			Frame = frame;
		}

		public Bitmap Frame { get; set; }
	}
}