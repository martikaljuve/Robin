namespace Robin.Core
{
	public interface IRobotController
	{
		void Update();
		IRobotCommander Commander { get; set; }
		VisionData VisionData { get; set; }
		SensorData SensorData { get; set; }
	}
}