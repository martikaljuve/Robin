using Stateless;

namespace Robin.RetroEncabulator
{
	public class Processor : IProcessor
	{
		private readonly StateMachine<State, Trigger> _stateMachine;

		public Processor()
		{
			_stateMachine = new StateMachine<State, Trigger>(State.LookingForBall);

			_stateMachine.Configure(State.LookingForBall)
				.Permit(Trigger.BallFound, State.ClosingInOnBall)
				.Permit(Trigger.BallCollected, State.FindingGoal);

			_stateMachine.Configure(State.ClosingInOnBall)
				.Permit(Trigger.BallLost, State.LookingForBall)
				.Permit(Trigger.BallCollected, State.FindingGoal);

			_stateMachine.Configure(State.FindingGoal)
				.Permit(Trigger.BallLaunched, State.LookingForBall)
				.Permit(Trigger.BallLost, State.LookingForBall);
			
		}

		public void Update(ArduinoSensorData data)
		{

		}
	}
}