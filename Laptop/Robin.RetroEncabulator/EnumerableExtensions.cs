using System;
using System.Collections.Generic;
using System.Linq;

namespace Robin.RetroEncabulator
{
	public static class EnumerableExtensions
	{
		public static T NextRandom<T>(this IEnumerable<T> source)
		{
			var gen = new Random();
			return source.Skip(gen.Next(0, source.Count() - 1) - 1).Take(1).First();
		}
	}
}