namespace Robin.RetroEncabulator
{
	public static class Sounds
	{
		public const string Pacman = @"Resources\pacman.wav";
		public const string R2D2a = @"Resources\r2d2-14.mp3";
		public const string R2D2b = @"Resources\r2d2a.wav";
		public const string SuperBomberman = @"Resources\sbomb-start.mid";
		public const string SuperMarioBros3Start = @"Resources\sm3-start.mid";
		public const string SuperMarioBrosTimeWarning = @"Resources\smb-time-warning.mid";

		public static readonly string[] IntroSounds =
			new[]
				{
					Pacman, 
					R2D2a, 
					R2D2b, 
					SuperBomberman, 
					SuperMarioBros3Start, 
					SuperMarioBrosTimeWarning
				};
	}
}