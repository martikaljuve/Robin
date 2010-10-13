namespace Robin
{
	public class ArduinoSensorData
	{
		public bool CoilgunCharged { get; set; }

		public bool BallInDribbler { get; set; }

		public bool BeaconIrLeftInView { get; set; }

		public bool BeaconIrRightInView { get; set; }

		public float? BeaconServoDirection { get; set; }

		public float? GyroDirection { get; set; }

		public bool OpponentBeaconFound
		{
			get { return BeaconIrLeftInView && BeaconIrRightInView; }
		}

		public void UpdateFromSerialData(string data)
		{
			var tokens = data.Split(' ');
			if (tokens.Length == 0) return;

			string first = null;
			if (tokens.Length >= 2) first = tokens[1];

			//string second;
			//if (tokens.Length >= 3) second = tokens[2];

			switch (tokens[0])
			{
				case ArduinoPrefix.CoilgunChargeStatus:
					CoilgunCharged = first != "0";
					break;
				case ArduinoPrefix.TripSensorStatus:
					BallInDribbler = first != "0";
					break;
				case ArduinoPrefix.GyroDirection:
					float gyroDirection;
					if (float.TryParse(first, out gyroDirection))
						GyroDirection = gyroDirection;
					else
						GyroDirection = null;
					break;
				case ArduinoPrefix.BeaconIrLeftInView:
					BeaconIrLeftInView = first != "0";
					break;
				case ArduinoPrefix.BeaconIrRightInView:
					BeaconIrRightInView = first != "0";
					break;
				case ArduinoPrefix.BeaconServoDirection:
					float servoDirection;
					if (float.TryParse(first, out servoDirection))
						BeaconServoDirection = servoDirection;
					else
						BeaconServoDirection = null;
					break;
			}
		}
	}
}