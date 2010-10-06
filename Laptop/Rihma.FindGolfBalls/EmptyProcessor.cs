using System.ComponentModel.Composition;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Rihma.FindGolfBalls
{
	[Export(typeof(IImageProcessor))]
	public class EmptyProcessor : IImageProcessor
	{
		public string Name
		{
			get { return "Do nothing"; }
		}

		public void Process(Image<Bgr, byte> sourceImage, ref Image<Bgr, byte> destinationImage)
		{
			
		}
	}
}