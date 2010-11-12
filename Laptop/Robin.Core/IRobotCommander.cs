namespace Robin.Core
{
	public interface IRobotCommander
	{
		void FireCoilgun(byte power);
		void Move(short direction, short speed);
		void Turn(short speed);
		void Stop();
		void SetDribbler(bool enabled);
		void MoveAndTurn(short direction, short moveSpeed, short turnSpeed);
		void SetIrChannel(byte channel);
		void SetState(byte state);
	}
}