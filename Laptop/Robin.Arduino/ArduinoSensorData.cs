using System;

namespace Robin.Arduino
{
	public class ArduinoSensorData
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
			
			ParseMessage(data);
		}

		private void ParseMessage(string data)
		{
			if (data.Length < 6) return;

			var firstByte = (byte) data[1];

			BallInDribbler = (1 & firstByte) == 1;
			BeaconIrLeftInView = (2 & firstByte) == 2;
			BeaconIrRightInView = (4 & firstByte) == 4;
			GyroDirection = BitConverter.ToInt16(new[] { (byte)data[2], (byte)data[3] }, 0);
			BeaconServoDirection = BitConverter.ToInt16(new[] { (byte)data[4], (byte)data[5] }, 0);
		}
	}
}