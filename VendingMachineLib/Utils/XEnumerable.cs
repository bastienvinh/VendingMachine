using System;
using System.Linq;
using System.Collections.Generic;

namespace Com.Bvinh.Linq
{

	/// <summary>
	/// Extensions for IEnumeralble
	/// </summary>
	public static class XEnumerable
	{
		/// <summary>
		/// Will loop and make any action for each elements of your Container.
		/// It's just a lazier way to loop your container.
		/// </summary>
		/// <param name="enumeration">Current Enumeration.</param>
		/// <param name="action">Any Action you want to do apply.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
				action(item);
		}
	}

	// Bastien : yes I know I am pretty lazy. But sometimes, I need something simple. Or course, it's just a test project.

	/// <summary>
	/// X (Extension) F(Framework) b(bvinh)
	/// </summary>
	public static partial class Xfb
	{
		/// <summary>
		/// Create a collection of number from 0 to the number
		/// Just to write less
		/// </summary>
		/// <param name="number">Number.</param>
		public static IEnumerable<int> Range(int number) => Enumerable.Range(0, number);
	}
}

