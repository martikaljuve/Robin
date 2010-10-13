using Robin.Arduino;

namespace Robin
{
	public interface IProcessor
	{
		ArduinoCommander Commander { get; set; }
		void Update(ArduinoSensorData data);
	}
}