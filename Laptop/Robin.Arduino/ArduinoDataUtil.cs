using System;
using System.Linq;
using Robin.Core;

namespace Robin.Arduino
{
	public static class ArduinoDataUtil
	{
		private static void UpdateFromSerialData(SensorData sensorData, string data)
		{
			if (data.Length < 6) return;
			if (!data.StartsWith(ArduinoPrefix.IncomingData)) return;

			var firstByte = (byte)data[1];

			sensorData.BallInDribbler = (1 & firstByte) == 1;
			sensorData.BeaconIrLeftInView = (2 & firstByte) == 2;
			sensorData.BeaconIrRightInView = (4 & firstByte) == 4;

			sensorData.GyroDirection = GetShortFromString(data, 2);
			sensorData.BeaconServoDirection = GetShortFromString(data, 4);
		}

		public static SensorData GetSensorDataFromString(string data)
		{
			var sensorData = new SensorData();

			if (string.IsNullOrEmpty(data))
				return sensorData;

			var tokens = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length == 0)
				return sensorData;

			var token = tokens.LastOrDefault(t => t.StartsWith("D"));
			if (token == default(string))
				return sensorData;

			UpdateFromSerialData(sensorData, token);
			return sensorData;
		}

		private static short GetShortFromString(string value, int index)
		{
			var bytes = new[] { (byte)value[index], (byte)value[index + 1] }.AsEnumerable();

			if (!BitConverter.IsLittleEndian)
				bytes = bytes.Reverse();

			return BitConverter.ToInt16(bytes.ToArray(), 0);
		}
	}
}