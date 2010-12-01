using System;
using System.Linq;
using Robin.Core;

namespace Robin.Arduino
{
	public static class ArduinoDataUtil
	{
		private static bool TryUpdateFromSerialData(SensorData sensorData, byte[] data)
		{
			if (data.Length < 12)
				return false;

			if ((char)data[0] != ArduinoPrefix.IncomingData)
				return false;

			if (!ValidateChecksum(data))
				return false;

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

			return true;
		}

		private static bool ValidateChecksum(byte[] data)
		{
			if (data.Length == 0)
				return false;

			byte checksum = 0;
			for (int i = 0; i < data.Length - 1; i++)
				checksum ^= data[i];

			return checksum == data[data.Length - 1];
		}

		public static SensorData GetSensorDataFromBytes(byte[] data)
		{
			var sensorData = new SensorData();

			if (data.Length == 0)
				return sensorData;

			byte previous = 0;
			int i;
			for (i = 0; i < data.Length; i++)
			{
				byte current = data[i];
				if (previous == '\r' && current == '\n')
					break;
				previous = current;
			}

			var bytes = data.Take(i - 1).ToArray();

			if (!TryUpdateFromSerialData(sensorData, bytes))
				return null;

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