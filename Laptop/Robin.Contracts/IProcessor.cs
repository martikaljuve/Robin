using Robin.Arduino;
using Robin.VideoProcessor;

namespace Robin
{
	public interface IProcessor
	{
		ArduinoCommander Commander { get; set; }
		VisionData VisionData { get; set; }
		ArduinoSensorData ArduinoData { get; set; }
		void Update();
		string ToDebugString();
	}
}