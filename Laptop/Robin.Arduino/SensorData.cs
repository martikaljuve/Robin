using System;

namespace Robin.Arduino
{
	public class SensorData
	{
		public bool BallInDribbler { get; set; }

		public bool BeaconIrLeftInView { get; set; }

		public bool BeaconIrRightInView { get; set; }

		public int BeaconServoDirection { get; set; }

		public int GyroDirection { get; set; }

		public bool OpponentBeaconFound
		{
			get { return BeaconIrLeftInView && BeaconIrRightInView; }
		}

		public void UpdateFromSerialData(string data)
		{
			if (string.IsNullOrEmpty(data)) return;
			if (data[0] != 'D') return;

			UpdateFromSerialData(this, data);
		}

		private static void UpdateFromSerialData(SensorData sensorData, string data)
		{
			if (data.Length < 6) return;

			var firstByte = (byte) data[1];

			sensorData.BallInDribbler = (1 & firstByte) == 1;
			sensorData.BeaconIrLeftInView = (2 & firstByte) == 2;
			sensorData.BeaconIrRightInView = (4 & firstByte) == 4;
			sensorData.GyroDirection = BitConverter.ToInt16(new[] { (byte)data[2], (byte)data[3] }, 0);
			sensorData.BeaconServoDirection = BitConverter.ToInt16(new[] { (byte)data[4], (byte)data[5] }, 0);
		}

		public static SensorData FromSerialData(string data)
		{
			var sensorData = new SensorData();
			sensorData.UpdateFromSerialData(data);
			return sensorData;
		}

		public override string ToString()
		{
			return string.Format("Drb:{0}, IrL:{1}, IrR:{2}, Srv:{3}, Gyr:{4}",
				BallInDribbler,
				BeaconIrLeftInView,
				BeaconIrRightInView,
				BeaconServoDirection,
				GyroDirection);
		}
	}
}