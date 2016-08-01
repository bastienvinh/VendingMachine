using System;
namespace Com.Bvinh.Vendingmachine
{
	public class ProductException : Exception
	{

		public ProductException(string message)
			: base(message)
		{
		}

		public ProductException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}
}

