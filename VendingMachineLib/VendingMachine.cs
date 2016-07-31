using System;
using System.Collections.Generic;
using Functional.Maybe;
using Com.Bvinh.Vendingmachine.Utils;

namespace Com.Bvinh.Vendingmachine
{

	using SeveralMoney = Tuple<int, Money>;
	using RestOfMoney = List<Tuple<int, Money>>;


	/// <summary>
	/// This a implementation of old vending machine model.
	/// We will represent a classic old Vending Machine system than you can see in any public place.
	/// T : represent the type of sotrage we need, because the storage is always fix.
	/// </summary>
	public class VendingMachine<T> : IVendingMachine
		where T :IStorageVMProducts
	{


		#region Attributes

		private int _numberOfReserve;
		private int _numberOfProducts;

		private Dictionary<string, T> _storageProducts; 

		// TODO : implements max capacity for each storage (IMPORTANTS)

		#endregion


		#region Properties


		/// <summary>
		/// This a the maximum of ISotrage we can have
		/// </summary>
		/// <value>The nummber of reserve.</value>
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

		#region Initialization

		private void Initialization()
		{
			_numberOfProducts = 0;
			_numberOfReserve = 0;
			_storageProducts = new Dictionary<string, T>();


		}

		#endregion


		#region Properties

		/// <summary>
		/// Tell if you have still more empty storage
		/// </summary>
		/// <value>The has empty storage.</value>
		public bool HasEmptyStorage
		{
			get { return (_numberOfReserve - _storageProducts.Count) > 0; }
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
				return _numberOfProducts;
			}
		}

		/// <summary>
		/// Return if a client can command and if there product for him.
		/// </summary>
		/// <value>The have products.</value>
		public bool HaveProducts
		{
			get { return _numberOfProducts > 0; }
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

		public void ClientPutMoney(Money m)
		{
			throw new NotImplementedException();
		}

		public RestOfMoney GetMoneyBackFromVM()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Storage

		/// <summary>
		/// We get an empty storage for the person that to fill the VM
		/// This will create too an empty place into the intern storage products
		/// Exception : Throw an exception in a case there no more Storage 
		/// </summary>
		/// <returns>The empty storage.</returns>
		public Maybe<T> GetEmptyStorage()
		{

			if (!HasEmptyStorage)
				throw VMExceptionUtils.NoMoreReserve();

			// We need a new key to rerieve our element on the dictionnary
			string newIndexStorage = SomeUtilsMethods.CreateCreateStringFromDate();

			// TODO : Continue this methods

			return Maybe<T>.Nothing;
		}

		/// <summary>
		/// Retrieve if you can place in one of the Storage
		/// </summary>
		/// <returns>The have place on AS torage.</returns>
		/// <param name="id">Identifier.</param>
		public bool StillHavePlaceOnAStorage(string id)
		{
			// TODO : fill this function
			return false;
		}

		#endregion


	}
}

