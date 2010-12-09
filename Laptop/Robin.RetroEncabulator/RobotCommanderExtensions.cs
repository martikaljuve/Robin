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
					commander.Turn(60);
					//commander.MoveAndTurn(340, 255, -11););
					break;
				case MovementRegion.TopCenterLeft:
					commander.Turn(30);
					//commander.MoveAndTurn(0, 255, -11);
					break;
				case MovementRegion.TopCenter:
					commander.MoveAndTurn(0, 500, 0);
					break;
				case MovementRegion.TopCenterRight:
					commander.Turn(-30);
					//commander.MoveAndTurn(0, 255, 11);
					break;
				case MovementRegion.TopRight:
					commander.Turn(-60);
					//commander.MoveAndTurn(20, 255, 11);
					break;
				case MovementRegion.BottomLeft:
					commander.Turn(90);
					//commander.MoveAndTurn(270, 100, 0);
					break;
				case MovementRegion.BottomCenterLeft:
					commander.Turn(60);
					//commander.MoveAndTurn(340, 100, 0);
					break;
				case MovementRegion.BottomCenter:
					commander.MoveAndTurn(0, 300, 0);
					break;
				case MovementRegion.BottomCenterRight:
					commander.Turn(-60);
					//commander.MoveAndTurn(20, 100, 0);
					break;
				case MovementRegion.BottomRight:
					commander.Turn(-90);
					//commander.MoveAndTurn(90, 100, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static void TurnTowardsZero(this IRobotCommander commander, int direction)
		{
			if (direction < 30)			commander.Turn(-30);
			else if (direction < 180)	commander.Turn(-60);
			else if (direction > 330)	commander.Turn(30);
			else                        commander.Turn(60);
		}
	}
}