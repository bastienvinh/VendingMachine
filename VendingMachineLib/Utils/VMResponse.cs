using System;
using Com.Bvinh.Vendingmachine.Utils;

namespace Com.Bvinh.Vendingmachine
{

	/// <summary>
	/// Class response from the vending machine when we retrieve our product
	/// </summary>
	public class VMResponse
	{

		#region Properties
		public bool HasProduct { get; internal set; }
		public bool HasError { get; internal set; }
		public VMErrorCode ErrorType { get; internal set; }
		public Product Product { get; internal set; }
		public IStorageVMProducts Storage { get; internal set; }
		#endregion

		#region Constructor

		public VMResponse()
		{
			HasError = false;
			HasProduct = false;
			ErrorType = VMErrorCode.NONE;
			Product = null;
			Storage = null;
		}

		#endregion

	}
}

