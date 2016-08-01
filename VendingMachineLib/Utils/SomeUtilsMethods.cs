using System;
namespace Com.Bvinh.Vendingmachine
{
	public static class SomeUtilsMethods
	{
		/// <summary>
		/// Create a string from a date and remove all specialcharacter to have something unique we can reuse
		/// </summary>
		/// <returns>New Transform Date String</returns>
		public static string CreateCreateStringFromDate() => DateTime.Now.ToString("yyyyMMddHHmmss");

		// Bastien : I don't know how I will generat my index so it's better to keep that apart.


		/// <summary>
		/// Generate a custom and unique identifier
		/// </summary>
		/// <returns>The unique identifier.</returns>
		public static string CreateUniqueIdentifier() => Guid.NewGuid().ToString().Replace('-', '\0');
	}
}

