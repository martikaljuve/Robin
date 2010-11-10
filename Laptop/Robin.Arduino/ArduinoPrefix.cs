namespace Robin.Arduino
{
	public static class ArduinoPrefix
	{
		public const string IncomingData = "D";
		
		public const string CoilgunFire = "F";

		public const string Move = "M";
		public const string MoveAndTurn = "G";
		public const string Turn = "T";
		public const string Stop = "S";

		public const string SetDribbler = "D";

		public const string SetIrChannel = "C";

		public const string SetState = "X";

		public static string[] NonRepeatableCommands = new[] { Move, MoveAndTurn, Turn };
	}
}