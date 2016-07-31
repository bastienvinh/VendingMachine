using System;
namespace Com.Bvinh.Vendingmachine
{

	// This is the basic exception for our vending machine

	public class VendingMachineException : Exception
	{

		public VendingMachineException(string message)
			: base(message)
		{
			
		}


		public VendingMachineException(string message, Exception innerException)
			: base(message, innerException)
		{
			
		}


	}
}

