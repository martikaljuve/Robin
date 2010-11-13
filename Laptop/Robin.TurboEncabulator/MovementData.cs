namespace Robin.RetroEncabulator
{
	public struct MovementData
	{
		public short Direction { get; set; }
		public short MoveSpeed { get; set; }
		public short TurnSpeed { get; set; }
		public bool DribblerEnabled { get; set; }

		public int Duration { get; set; }
	}
}
