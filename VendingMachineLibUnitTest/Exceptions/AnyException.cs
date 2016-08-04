using System;
namespace VendingMachineLibUnitTest
{
	public class AnyException : Exception
	{
		public AnyException(string message)
			: base(message)
		{
		}

		public AnyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

