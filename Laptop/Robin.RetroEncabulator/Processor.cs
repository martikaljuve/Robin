using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using Robin.Arduino;
using Stateless;
using Timer = System.Timers.Timer;

namespace Robin.RetroEncabulator
{
	[Export(typeof(IProcessor))]
	public class Processor : IProcessor
	{
		private readonly StateMachine<State, Trigger> _stateMachine;
		private readonly Timer _timer;
		
		private VisionData _visionData = new VisionData();

		public Processor()
		{
			_timer = new Timer();

			_stateMachine = new StateMachine<State, Trigger>(State.LookingForBall);

			_stateMachine.Configure(State.LookingForBall)
				.Permit(Trigger.BallFound, State.ClosingInOnBall)
				.Permit(Trigger.BallCollected, State.FindingGoal);

			_stateMachine.Configure(State.ClosingInOnBall)
				.Permit(Trigger.BallLost, State.LookingForBall)
				.Permit(Trigger.BallCollected, State.FindingGoal);

			_stateMachine.Configure(State.FindingGoal)
				.Permit(Trigger.BallLaunched, State.LookingForBall)
				.Permit(Trigger.BallLost, State.LookingForBall)
				.Permit(Trigger.Timeout, State.LookingForBall)
				.OnEntry(() => ResetTimerTo(5000, LaunchBall))
				.OnExit(() => _timer.Stop());
		}

		private void ResetTimerTo(double milliseconds, Action action)
		{
			_timer.Stop();
			_timer.Interval = milliseconds;
			_timer.Start();
			_timer.Elapsed += (sender, args) => action();
		}

		public ArduinoCommander Commander { get; set; }

		public void Update(ArduinoSensorData data)
		{
			switch (_stateMachine.State)
			{
				case State.LookingForBall:
					LookingForBall(data);
					break;
				case State.ClosingInOnBall:
					ClosingInOnBall(data);
					break;
				case State.FindingGoal:
					FindingGoal(data, _visionData);
					break;
			}

			// HACK: Testime
			Thread.Sleep(100);
		}

		private void LookingForBall(ArduinoSensorData data)
		{
			if (data.BallInDribbler)
				_stateMachine.Fire(Trigger.BallCollected);
		}

		private void ClosingInOnBall(ArduinoSensorData data)
		{
			if (data.BallInDribbler)
				_stateMachine.Fire(Trigger.BallCollected);
		}

		private void FindingGoal(ArduinoSensorData arduinoData, VisionData visionData)
		{
			if (arduinoData.GyroDirection == 0 && visionData.OpponentGoalInFront)
				Commander.FireCoilgun();
		}

		private void LaunchBall()
		{
			Commander.FireCoilgun();
			_stateMachine.Fire(Trigger.BallLaunched);
		}
	}
}