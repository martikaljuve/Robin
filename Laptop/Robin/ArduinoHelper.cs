namespace Robin
{
	public static class ArduinoSerialExtensions
	{
		public static void Move(this ArduinoSerial arduino, float direction, int speed, float distance)
		{
			arduino.Command(ArduinoCommands.Move, direction, speed, distance);
		}

		public static void Turn(this ArduinoSerial arduino, float degrees, int speed)
		{
			arduino.Command(ArduinoCommands.Turn, degrees, speed);
		}
	}
}