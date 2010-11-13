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
					commander.MoveAndTurn(340, 255, -11);
					break;
				case MovementRegion.TopCenterLeft:
					commander.MoveAndTurn(0, 255, -11);
					break;
				case MovementRegion.TopCenter:
					commander.MoveAndTurn(0, 255, 0);
					break;
				case MovementRegion.TopCenterRight:
					commander.MoveAndTurn(0, 255, 11);
					break;
				case MovementRegion.TopRight:
					commander.MoveAndTurn(20, 255, 11);
					break;
				case MovementRegion.BottomLeft:
					commander.MoveAndTurn(270, 100, 0);
					break;
				case MovementRegion.BottomCenterLeft:
					commander.MoveAndTurn(340, 100, 0);
					break;
				case MovementRegion.BottomCenter:
					commander.MoveAndTurn(0, 100, 0);
					break;
				case MovementRegion.BottomCenterRight:
					commander.MoveAndTurn(20, 100, 0);
					break;
				case MovementRegion.BottomRight:
					commander.MoveAndTurn(90, 100, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}