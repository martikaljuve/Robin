using System;

namespace Robin.Core
{
	[Flags]
	public enum Colors : byte
	{
		Red = 4,
		Green = 1,
		Blue = 2,
		Magenta = Red | Blue,
		Cyan = Green | Blue,
		Yellow = Red | Green,
		White = Red | Green | Blue
	}
}