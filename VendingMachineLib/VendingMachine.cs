using System;
namespace Com.Bvinh.Vendingmachine
{


	// This class represent a vending machine

	/// <summary>
	/// This a implementation of old vending machine model.
	/// </summary>
	public class VendingMachine : IVendingMachine
	{


		#region Attributes

		private int _numberOfReserve = 0;

		#endregion


		#region Properties

		// TODO : implement this region

		public int NummberOfReserve
		{
			get { return _numberOfReserve; }
		}


		#endregion

		#region Constructor

		public VendingMachine(int numberOfReserve)
		{
			_numberOfReserve = numberOfReserve;
		}

		#endregion

		#region Implements IVendingMachine

		public double MaxMoney
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public int NumberMaxProducts
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int NumberOfProductsLeft
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool AddProduct(Product product, int IdStorage)
		{
			throw new NotImplementedException();
		}

		public double GetCurrentClientMoney()
		{
			throw new NotImplementedException();
		}

		public int GetNumberOfProductFromStorage(int idStorage)
		{
			throw new NotImplementedException();
		}

		public double GetTotalMoneyClient()
		{
			throw new NotImplementedException();
		}

		public double GetTotalMoneyMachine()
		{
			throw new NotImplementedException();
		}

		public void GiveBackClientMoney()
		{
			throw new NotImplementedException();
		}

		public double RemoveAllProducts()
		{
			throw new NotImplementedException();
		}

		public bool RemoveProduct(Product product, int IdStorage)
		{
			throw new NotImplementedException();
		}

		public void ResetMoneyClientHasSpent()
		{
			throw new NotImplementedException();
		}

		#endregion


	}
}

