namespace Robin
{
	public static class ArduinoHelper
	{
		private static ArduinoSerial _arduino;
		private const string ResultNotAvailable = "-1";
		private const string ResultTrue = "1";

		public static void SetArduino(ArduinoSerial serial)
		{
			_arduino = serial;
		}

		public static void Move(float direction, int speed, float distance)
		{
			_arduino.Command(ArduinoCommands.Move, direction, speed, distance);
		}

		public static void Turn(float degrees, int speed)
		{
			_arduino.Command(ArduinoCommands.Turn, degrees, speed);
		}

		public static float GetBeaconServoRotation()
		{
			var result = _arduino.Query(ArduinoQueries.BeaconServoRotation);

			return FloatOrNan(result);
		}

		public static bool GetTripSensorStatus()
		{
			var result = _arduino.Query(ArduinoQueries.TripSensor);
			return result == ResultTrue ? true : false;
		}

		private static float FloatOrNan(string value)
		{
			if (value != null && value != ResultNotAvailable)
			{
				float res;
				if (float.TryParse(value, out res))
					return res;
			}

			return float.NaN;
		}
	}
}