using System;

namespace Robin.Core
{
	[Flags]
	public enum States : byte
	{
		None = 0x0,
		LedRed = 0x1,
		LedGreen = 0x2,
		LedBlue = 0x4
	}
}