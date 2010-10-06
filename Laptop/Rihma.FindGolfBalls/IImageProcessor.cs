using Emgu.CV;
using Emgu.CV.Structure;

namespace Rihma.FindGolfBalls
{
	public interface IImageProcessor
	{
		string Name { get; }
		void Process(Image<Bgr, byte> sourceImage, ref Image<Bgr, byte> destinationImage);
	}
}