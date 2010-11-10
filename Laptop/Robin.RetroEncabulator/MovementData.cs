namespace Robin.RetroEncabulator
{
	public struct MovementData
	{
		public int Direction { get; set; }
		public int MoveSpeed { get; set; }
		public int TurnSpeed { get; set; }
		public bool DribblerEnabled { get; set; }

		public int Duration { get; set; }
	}
}
