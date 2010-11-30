using Robin.Core;

namespace Robin.Arduino
{
	public class ArduinoCommander : IRobotCommander
	{
		private readonly ArduinoSerial arduino;

		public ArduinoCommander(ArduinoSerial arduino)
		{
			this.arduino = arduino;
		}

		public void FireCoilgun(byte power)
		{
			arduino.Command(ArduinoPrefix.CoilgunFire, power);
		}

		public void Move(short localDirectionInDegrees, short distance)
		{
			arduino.Command(ArduinoPrefix.Move, localDirectionInDegrees, distance);
		}

		public void Turn(short localDirectionInDegrees)
		{
			arduino.Command(ArduinoPrefix.Turn, localDirectionInDegrees);
		}
		
		public void Stop()
		{
			arduino.Command(ArduinoPrefix.Stop);
		}

		public void SetDribbler(bool enabled)
		{
			arduino.Command(ArduinoPrefix.SetDribbler, enabled ? (short)255 : (short)0);
		}

		public void SetDribbler(short speed)
		{
			arduino.Command(ArduinoPrefix.SetDribbler, speed);
		}

		public void MoveAndTurn(short localDirectionInDegrees, short distance, short localRotationInDegrees)
		{
			arduino.Command(ArduinoPrefix.MoveAndTurn, localDirectionInDegrees, distance, localRotationInDegrees);
		}

		public void SetIrChannel(byte channel)
		{
			arduino.Command(ArduinoPrefix.SetIrChannel, channel);
		}

		public void SetColors(Colors colors)
		{
			arduino.Command(ArduinoPrefix.SetState, colors);
		}
		
		public ArduinoSerial ArduinoSerial
		{
			get { return arduino; }
		}
	}
}