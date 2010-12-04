using System.Diagnostics;
using System.IO;
using System.Media;

namespace Robin.RetroEncabulator
{
	public static class SoundClipPlayer
	{
		private static long soundEndTime;
		private static readonly SoundPlayer Player = new SoundPlayer();
		private static readonly Stopwatch Stopwatch = new Stopwatch();

		static SoundClipPlayer()
		{
			Stopwatch.Start();
		}

		public static void PlayIntro()
		{
			var files = Directory.GetFiles("Resources", "*.wav");
			if (files.Length == 0)
				return;

			Player.SoundLocation = files.NextRandom();
			Player.Play();
		}

		public static void PlayAlarm()
		{
			if (Stopwatch.ElapsedMilliseconds < soundEndTime)
				return;

			Player.SoundLocation = "Resources\\Sounds\\Alarm.wav";
			Player.Play();
			soundEndTime = Stopwatch.ElapsedMilliseconds + 3000;
		}
	}
}