using System;
using System.Collections.Generic;

namespace Com.Bvinh.Vendingmachine.Utils
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
}

