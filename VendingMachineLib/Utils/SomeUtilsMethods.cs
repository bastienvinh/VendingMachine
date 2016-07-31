using System;
namespace Com.Bvinh.Vendingmachine
{
	public static class SomeUtilsMethods
	{
		/// <summary>
		/// Create a string from a date and remove all specialcharacter to have something unique we can reuse
		/// </summary>
		/// <returns>New Transform Date String</returns>
		public static string CreateCreateStringFromDate()
		{
			// Bastien : I don't know how I will generat my index so it's better to keep that apart.

			return DateTime.Now.ToString("yyyyMMddHHmmss");
		}
	}
}

