using System.IO;
using System.Media;

namespace Robin.RetroEncabulator
{
	public static class SoundClipPlayer
	{
		private static readonly SoundPlayer Player = new SoundPlayer();

		public static void PlayIntro()
		{
			var files = Directory.GetFiles("Resources", "*.wav");
			if (files.Length == 0)
				return;

			Player.SoundLocation = files.NextRandom();
			Player.Play();
		}
	}
}