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

		#region Predefined Response
		// We use prefined response we can shorted our response.


		/// <summary>
		/// Create a response from Vending Machine, the client can have his product.
		/// </summary>
		/// <returns>The has product.</returns>
		/// <param name="storage">Storage.</param>
		/// <param name="p">P.</param>
		public static VMResponse CreateHasProduct(IStorageVMProducts storage, Product p = null)
		{
			return new VMResponse
			{
				ErrorType = VMErrorCode.NONE,
				HasError  = false,
				HasProduct = true,
				Product = p,
				Storage = storage
			};
		}

		/// <summary>
		/// Create a predefined response from Vending Machine, there are no product anymore in this storage.
		/// </summary>
		/// <returns>The no product anymore.</returns>
		/// <param name="storage">Storage.</param>
		/// <param name="p">P.</param>
		public static VMResponse CreateNoProductAnymore(IStorageVMProducts storage, Product p = null)
		{
			return new VMResponse
			{
				ErrorType = VMErrorCode.NO_MORE_PRODUCTS,
				HasError = true,
				HasProduct = false,
				Product = p,
				Storage = storage
			};
		}

		#endregion

	}
}

