using System.Media;

namespace Robin.RetroEncabulator
{
	public static class SoundClipPlayer
	{
		private static SoundPlayer player = new SoundPlayer();

		public static void PlayIntro()
		{
			var randomSound = Sounds.IntroSounds.NextRandom();
			player.SoundLocation = randomSound;
			player.Play();
		}
	}
}