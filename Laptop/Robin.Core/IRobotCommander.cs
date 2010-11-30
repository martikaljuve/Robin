namespace Robin.Core
{
	public interface IRobotCommander
	{
		void FireCoilgun(byte power);
		void Turn(short localDirectionInDegrees);
		void Move(short localDirectionInDegrees, short distance);
		void MoveAndTurn(short localDirectionInDegrees, short distance, short localRotationInDegrees);
		void Stop();
		void SetDribbler(bool enabled);
		void SetDribbler(short speed);
		void SetIrChannel(byte channel);
		void SetColors(Colors colors);
	}
}