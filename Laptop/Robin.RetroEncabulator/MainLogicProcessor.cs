using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Robin.Arduino;
using Robin.VideoProcessor;
using Stateless;
using System.Timers;

namespace Robin.RetroEncabulator
{
	[Export(typeof(IProcessor))]
	public class MainLogicProcessor : IProcessor
	{
		private readonly StateMachine<State, Trigger> stateMachine;
		private readonly Timer timer;
		
		public MainLogicProcessor()
		{
			timer = new Timer();

			stateMachine = new StateMachine<State, Trigger>(State.LookingForBall);

			stateMachine.Configure(State.LookingForBall)
				.Permit(Trigger.CameraLockedOnBall, State.ClosingInOnBall)
				.Permit(Trigger.BallCaught, State.FindingGoal);

			stateMachine.Configure(State.ClosingInOnBall)
				.Permit(Trigger.CameraLostBall, State.LookingForBall)
				.Permit(Trigger.BallCaught, State.FindingGoal)
				.Permit(Trigger.Timeout, State.LookingForBall)
				.OnEntry(() => StartTimer(10000, Trigger.Timeout))
				.OnExit(StopTimer);

			stateMachine.Configure(State.FindingGoal)
				.Permit(Trigger.CoilgunLaunched, State.LookingForBall)
				.Permit(Trigger.BallLost, State.LookingForBall)
				.Permit(Trigger.Timeout, State.LookingForBall)
				.OnEntry(() => StartTimer(5000, Trigger.Timeout))
				.OnExit(StopTimer);
		}

		private readonly Dictionary<State, State> sources = new Dictionary<State, State>();
		public string ToDebugString()
		{
			//return stateMachine.ToString();
			var str = new StringBuilder();

			stateMachine.Configure(State.LookingForBall)
				.OnEntry(StoreSourceState)
				.PermitDynamic(Trigger.DebugBack, () => GetSource(State.LookingForBall))
				.Permit(Trigger.DebugForward, State.ClosingInOnBall)
				.OnEntry(() => { });
			stateMachine.Configure(State.ClosingInOnBall)
				.OnEntry(StoreSourceState)
				.PermitDynamic(Trigger.DebugBack, () => GetSource(State.ClosingInOnBall))
				.Permit(Trigger.DebugForward, State.FindingGoal)
				.OnEntry(() => { });
			stateMachine.Configure(State.FindingGoal)
				.OnEntry(StoreSourceState)
				.PermitDynamic(Trigger.DebugBack, () => GetSource(State.FindingGoal))
				.Permit(Trigger.DebugForward, State.LookingForBall)
				.OnEntry(() => { });

			var allStates = Enum.GetValues(typeof(State));
			var allTriggers = Enum.GetValues(typeof(Trigger));
			for (int i = 0; i < allStates.Length; i++)
			{
				var state = stateMachine.State;

				foreach (Trigger trigger in allTriggers)
				{
					if (trigger == Trigger.DebugBack || trigger == Trigger.DebugForward) continue;
					if (!stateMachine.CanFire(trigger)) continue;

					stateMachine.Fire(trigger);
					str.AppendFormat("[{0}]-{1}->[{2}]", state, trigger, stateMachine.State);
					str.AppendLine();
					stateMachine.Fire(Trigger.DebugBack);
				}

				stateMachine.Fire(Trigger.DebugForward);
			}
			return str.ToString();
		}

		private void StoreSourceState(StateMachine<State, Trigger>.Transition transition)
		{
			if (!sources.ContainsKey(transition.Destination))
				sources.Add(transition.Destination, transition.Source);
			else
				sources[transition.Destination] = transition.Source;
		}

		private State GetSource(State destination)
		{
			var source = sources[destination];
			return source;
		}

		private void StartTimer(double milliseconds, Action action)
		{
			timer.Stop();
			timer.Interval = milliseconds;
			timer.Start();
			timer.Elapsed += (sender, args) => action();
		}

		private void StartTimer(double milliseconds, Trigger trigger)
		{
			StartTimer(milliseconds, () => stateMachine.Fire(trigger));
		}

		private void StopTimer()
		{
			timer.Stop();
		}

		public ArduinoCommander Commander { get; set; }

		public void Update()
		{
			switch (stateMachine.State)
			{
				case State.LookingForBall:
					LookingForBall();
					break;
				case State.ClosingInOnBall:
					ClosingInOnBall();
					break;
				case State.FindingGoal:
					FindingGoal();
					break;
			}

			// HACK: Testime
			System.Threading.Thread.Sleep(100);
		}

		private void LookingForBall()
		{
			if (ArduinoData.BallInDribbler)
				stateMachine.Fire(Trigger.BallCaught);
		}

		private void ClosingInOnBall()
		{
			if (ArduinoData.BallInDribbler)
				stateMachine.Fire(Trigger.BallCaught);
		}

		private void FindingGoal()
		{
			if (ArduinoData.BeaconServoDirection == 0 && VisionData.OpponentGoalInFront)
				LaunchBall();
		}

		private void LaunchBall()
		{
			Commander.FireCoilgun(50);
			stateMachine.Fire(Trigger.CoilgunLaunched);
		}

		public VisionData VisionData { get; set; }

		public ArduinoSensorData ArduinoData { get; set; }
	}
}