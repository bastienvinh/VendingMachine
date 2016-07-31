using System;
namespace Com.Bvinh.Vendingmachine
{
	public class MoneyException : Exception
	{
		public MoneyException(string message)
			: base(message)
		{
		}

		public MoneyException(string message, Exception innerException)
			: base(message, innerException)
		{
			
		}


	}
}

