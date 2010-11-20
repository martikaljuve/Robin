namespace Robin.Core
{
	public class SensorData
	{
		public bool BallInDribbler { get; set; }

		public bool BeaconIrLeftInView { get; set; }

		public bool BeaconIrRightInView { get; set; }

		public short BeaconServoDirection { get; set; }

		public short GyroDirection { get; set; }

		public bool OpponentBeaconFound
		{
			get { return BeaconIrLeftInView && BeaconIrRightInView; }
		}

		public bool IsPowered { get; set; }

		public byte IrChannel { get; set; }

		public override string ToString()
		{
			return string.Format("Drb:{0}, IrL:{1}, IrR:{2}, Srv:{3}, Gyr:{4}, Pow:{5}, IrC: {6}",
				BallInDribbler,
				BeaconIrLeftInView,
				BeaconIrRightInView,
				BeaconServoDirection,
				GyroDirection,
				IsPowered,
				IrChannel);
		}
	}
}