using System;
namespace Com.Bvinh.Vendingmachine
{

	/// <summary>
	/// Generic Exception for IStorageVendingMachine
	/// </summary>
	public class StorageException : Exception
	{
		public StorageException(string message)
			: base(message)
		{
		}

		public StorageException(string message, Exception innerException)
			: base(message, innerException)
		{
			
		}
	}
}

