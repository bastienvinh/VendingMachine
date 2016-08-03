using System;
using Com.Bvinh.Vendingmachine.Exceptions;

namespace Com.Bvinh.Vendingmachine
{

	// This is the basic exception for our vending machine

	/// <summary>
	/// Generic VM Exception and basic of all
	/// </summary>
	public class VendingMachineException : Exception
	{
		public VMErrorCode CodeError { get; protected set; }

		public VendingMachineException(string message)
			: base(message)
		{
			CodeError = VMErrorCode.UNKNOWN_ERROR;
		}


		public VendingMachineException(string message, Exception innerException)
			: base(message, innerException)
		{
			CodeError = VMErrorCode.UNKNOWN_ERROR;
		}


		public VendingMachineException(VMErrorCode code, string message, Exception innerException)
			: this (message, innerException)
		{
			CodeError = code;
		}
	}


	/// <summary>
	/// Exception that the client made like : No More enough money / Client push bad command
	/// </summary>
	public class VMClientTypeException : VendingMachineException
	{
		// Bastien : it's up to you to use it or not

		public VMClientTypeException(VMErrorCode code, string message, Exception innerException)
			: base( code, message, innerException )
		{
			//CodeError = code;
		}

		public VMClientTypeException(VMErrorCode code, string message)
			: base(code, message, null)
		{
		}
	}

	/// <summary>
	/// Exception made by the supplier
	/// <example>Bad Storage issue</example>
	/// </summary>
	public class VMSupplierProductTypeException : VendingMachineException
	{
		// Bastien : it's up to you to use it or not

		public VMSupplierProductTypeException(VMErrorCode code, string message, Exception innerException)
			: base(code, message, innerException)
		{
			//CodeError = code;
		}

		public VMSupplierProductTypeException(VMErrorCode code, string message)
			: base(code, message, null)
		{
		}
	}

}

