using System;
using System.Drawing;
using Robin.Core;

namespace Robin.RetroEncabulator
{
	public static class RobotCommanderExtensions
	{
		public static void MoveToVisionLocation(this IRobotCommander commander, Point trackedBallLocation)
		{
			switch (MovementRegions.GetRegionFromPoint(trackedBallLocation))
			{
				case MovementRegion.None:
					commander.Stop();
					break;
				case MovementRegion.TopLeft:
					commander.Turn(10);
					//commander.MoveAndTurn(340, 255, -11););
					break;
				case MovementRegion.TopCenterLeft:
					commander.Turn(5);
					//commander.MoveAndTurn(0, 255, -11);
					break;
				case MovementRegion.TopCenter:
					commander.MoveAndTurn(0, 500, 0);
					break;
				case MovementRegion.TopCenterRight:
					commander.Turn(-5);
					//commander.MoveAndTurn(0, 255, 11);
					break;
				case MovementRegion.TopRight:
					commander.Turn(-10);
					//commander.MoveAndTurn(20, 255, 11);
					break;
				case MovementRegion.BottomLeft:
					commander.Turn(15);
					//commander.MoveAndTurn(270, 100, 0);
					break;
				case MovementRegion.BottomCenterLeft:
					commander.Turn(10);
					//commander.MoveAndTurn(340, 100, 0);
					break;
				case MovementRegion.BottomCenter:
					commander.MoveAndTurn(0, 100, 0);
					break;
				case MovementRegion.BottomCenterRight:
					commander.Turn(-10);
					//commander.MoveAndTurn(20, 100, 0);
					break;
				case MovementRegion.BottomRight:
					commander.Turn(-15);
					//commander.MoveAndTurn(90, 100, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static void TurnTowardsGoal(this IRobotCommander commander, Point estimatedPosition, short estimatedDirection)
		{
			var goalPosition = new Point(-1000, 3100);
			var x = goalPosition.X - estimatedPosition.X;
			var y = goalPosition.Y - estimatedPosition.Y;
			var newDirection = Math.Atan2(x, y) * Math.PI / 180;

			var diff = newDirection - estimatedDirection;
			if (diff > 180)
				diff -= 360;
			if (diff < -180)
				diff += 360;
			commander.Turn((short) diff);
		}
	}
}