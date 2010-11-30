using System;
using System.Drawing;

namespace Robin.Core
{
	public class SensorData
	{
		public bool BallInDribbler { get; set; }

		public bool BeaconIrLeftInView { get; set; }

		public bool BeaconIrRightInView { get; set; }

		public short BeaconServoDirection { get; set; }

		public bool OpponentBeaconFound
		{
			get { return BeaconIrLeftInView && BeaconIrRightInView; }
		}

		public bool IsPowered { get; set; }

		public byte IrChannel { get; set; }

		public short EstimatedGlobalDirection { get; set; }

		// Coordinate system:
		//    ^ +Y
		//    |
		// <--+--> +X
		//    |        |
		//    V      <- +Theta

		public short EstimatedGlobalX { get; set; }

		public short EstimatedGlobalY { get; set; }

		public Point EstimatedGlobalPosition
		{
			get { return new Point(EstimatedGlobalX, EstimatedGlobalY); }
		}

		public override string ToString()
		{
			return string.Format("Drb:{0}, IrL:{1}, IrR:{2}, Srv:{3}, Dir:{4}, Pos:{5}x{6}, Pow:{7}, IrC: {8}",
				BallInDribbler,
				BeaconIrLeftInView,
				BeaconIrRightInView,
				BeaconServoDirection,
				EstimatedGlobalDirection,
				EstimatedGlobalX,
				EstimatedGlobalY,
				IsPowered,
				IrChannel);
		}
	}
}