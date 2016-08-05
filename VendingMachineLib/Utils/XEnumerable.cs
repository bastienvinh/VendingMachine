using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Com.Bvinh.Linq
{

	/// <summary>
	/// Extensions for IEnumeralble
	/// </summary>
	public static partial class XEnumerable
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
			{
				if (action != null) action(item);
			}
		}

		/// <summary>
		/// For Any dictionnary
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="action">Action.</param>
		public static void ForEach<Tkey, TValue>(this IDictionary<Tkey, TValue> source, Action<Tkey, TValue> action)
		{
			if (action != null)
			{
				foreach (KeyValuePair<Tkey, TValue> p in source) action(p.Key, p.Value);
			}
		}

		public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TKey, TValue> action)
		{
			if (action != null)
			{
				foreach (KeyValuePair<TKey, TValue> p in source)
					action(p.Key, p.Value);
			}
		}

		/// <summary>
		/// Check if a collection is empty or not
		/// </summary>
		/// <returns><c>true</c>, The collection is empty, <c>false</c> The Collection is not empty.</returns>
		/// <param name="source">Current collection</param>
		public static bool IsNotNullOrEmpty(this IEnumerable<object> source) => source != null && source.Any();

		// TODO : Create a methods HasValue
		//public static bool HasValue( Pro )
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


		/// <summary>
		/// Creates a new KeyValue<TKey, TValue> pair.
		/// </summary>
		/// <returns>The new pair.</returns>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		/// <typeparam name="TKey">Type of the key</typeparam>
		/// <typeparam name="TValue">Type of the value</typeparam>
		public static KeyValuePair<TKey, TValue> CreateNewPair<TKey, TValue>(TKey key, TValue value)
		=> new KeyValuePair<TKey, TValue>(key, value);
	}
}

