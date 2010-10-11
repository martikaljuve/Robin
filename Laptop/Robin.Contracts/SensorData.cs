namespace Robin
{
	public class SensorData
	{
		public bool OpponentBeaconFound { get; set; }
		public short OpponentBeaconDirection { get; set; }
		public bool BallInDribbler { get; set; }
		public ushort GyroDirection { get; set; }
	}
}