using System;

namespace Robin.Core
{
	public interface IRobotController : IDisposable
	{
		void Update();
		IntPtr Parent { get; set; }
		IRobotCommander Commander { get; set; }
		VisionData VisionData { get; set; }
		SensorData SensorData { get; set; }
		string Name { get; }
	}
}