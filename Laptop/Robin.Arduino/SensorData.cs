using System;
using System.Linq;

namespace Robin.Arduino
{
	public class SensorData
	{
		public bool BallInDribbler { get; set; }

		public bool BeaconIrLeftInView { get; set; }

		public bool BeaconIrRightInView { get; set; }

		public short BeaconServoDirection { get; set; }

		public short GyroDirection { get; set; }

		public bool OpponentBeaconFound
		{
			get { return BeaconIrLeftInView && BeaconIrRightInView; }
		}

		public void UpdateFromSerialData(string data)
		{
			if (string.IsNullOrEmpty(data)) return;
			var tokens = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length == 0) return;
			var token = tokens.LastOrDefault(t => t.StartsWith("D"));
			if (token == default(string)) return;

			UpdateFromSerialData(this, token);
		}

		private static void UpdateFromSerialData(SensorData sensorData, string data)
		{
			if (data.Length < 6) return;
			if (!data.StartsWith(ArduinoPrefix.IncomingData)) return;

			var firstByte = (byte) data[1];

			sensorData.BallInDribbler = (1 & firstByte) == 1;
			sensorData.BeaconIrLeftInView = (2 & firstByte) == 2;
			sensorData.BeaconIrRightInView = (4 & firstByte) == 4;

			sensorData.GyroDirection = GetShortFromString(data, 2);
			sensorData.BeaconServoDirection = GetShortFromString(data, 4);
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

		public static short GetShortFromString(string value, int index)
		{
			var bytes = new[] { (byte)value[index], (byte)value[index + 1] }.AsEnumerable();

			if (!BitConverter.IsLittleEndian)
				bytes = bytes.Reverse();

			return BitConverter.ToInt16(bytes.ToArray(), 0);
		}
	}
}