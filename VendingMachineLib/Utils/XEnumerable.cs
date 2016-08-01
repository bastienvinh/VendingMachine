using System;
using System.Collections.Generic;

namespace Com.Bvinh.Vendingmachine.Utils
{

	/// <summary>
	/// Extensions for IEnumeralble
	/// </summary>
	public static class XEnumerable
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
				action(item);
		}
	}
}

