using System;
using System.Linq;
using System.Text;
using Robin.Core;

namespace Robin.Arduino
{
	public static class ArduinoDataUtil
	{
		private static void UpdateFromSerialData(SensorData sensorData, byte[] data)
		{
			if (data.Length < 6) return;
			if ((char)data[0] != ArduinoPrefix.IncomingData) return;

			var firstByte = data[1];

			sensorData.BallInDribbler = (1 & firstByte) == 1;
			sensorData.BeaconIrLeftInView = (2 & firstByte) == 2;
			sensorData.BeaconIrRightInView = (4 & firstByte) == 4;
			sensorData.IsPowered = (8 & firstByte) == 8;

			sensorData.IrChannel = data[2];

			sensorData.EstimatedGlobalX = GetShortFromBytes(data, 3);
			sensorData.EstimatedGlobalY = GetShortFromBytes(data, 5);
			sensorData.EstimatedGlobalDirection = GetShortFromBytes(data, 7);
			sensorData.BeaconServoDirection = GetShortFromBytes(data, 9);
		}

		public static SensorData GetSensorDataFromBytes(byte[] data)
		{
			var sensorData = new SensorData();

			if (data.Length == 0)
				return sensorData;

			var bytes = data.TakeWhile(x => (char) x != '\n').ToArray();

			UpdateFromSerialData(sensorData, bytes);
			return sensorData;
		}

		private static short GetShortFromBytes(byte[] bytes, int index)
		{
			var data = bytes.Skip(index).Take(2).ToArray();
			if (data.Length < 2)
				return 0;

			if (!BitConverter.IsLittleEndian)
				data = data.Reverse().ToArray();

			return BitConverter.ToInt16(data, 0);
		}
	}
}