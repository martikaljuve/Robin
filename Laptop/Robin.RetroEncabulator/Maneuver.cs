using System.Collections.Generic;
using System.Diagnostics;
using Robin.Arduino;

namespace Robin.RetroEncabulator
{
	public class Maneuver
	{
		private readonly LinkedList<MovementData> moves = new LinkedList<MovementData>();
		private LinkedListNode<MovementData> currentMoveNode;
		private Stopwatch timer = new Stopwatch();
		private ArduinoCommander commander;

		public bool IsDone { get; set; }

		public Maneuver(ArduinoCommander commander)
		{
			this.commander = commander;
		}

		public void Start()
		{
			timer.Restart();

			currentMoveNode = moves.First;
			Execute(CurrentMove);
		}

		public void Update()
		{
			if (timer.ElapsedMilliseconds < CurrentMove.Duration)
				return;

			currentMoveNode = currentMoveNode.Next;
			Execute(CurrentMove);
			timer.Restart();
		}

		private void Execute(MovementData data)
		{
			commander.MoveAndTurn(data.Direction, data.MoveSpeed, data.TurnSpeed);
			commander.SetDribbler(data.DribblerEnabled);
		}

		private MovementData CurrentMove
		{
			get { return currentMoveNode.Value; }
		}

		public LinkedList<MovementData> Moves
		{
			get { return moves; }
		}
	}
}
