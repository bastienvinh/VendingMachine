using System;
using System.Collections.Generic;
using System.Linq;
using Functional.Maybe;
using Com.Bvinh.Linq;
using Com.Bvinh.Vendingmachine.Exceptions;

namespace Com.Bvinh.Vendingmachine
{

	using SeveralMoney = Tuple<int, Money>;
	using RestOfMoney = List<Tuple<int, Money>>;
	using StorageMoneyTuple = Dictionary<Money, int>;


	/// <summary>
	/// This a implementation of old vending machine model.
	/// We will represent a classic old Vending Machine system than you can see in any public place.
	/// T : represent the type of sotrage we need, because the storage is always fix.
	/// </summary>
	public class VendingMachine<T> : IVendingMachine
		where T :IStorageVMProducts, IStoragePriceVMProducts
	{


		#region Attributes

		private int _numberOfReserve;

		// Products
		private int _numberOfProducts;
		private int _numberMaxProductsByStorage;

		private Dictionary<string, T> _storageProducts;
		private StorageMoneyTuple _listMoneysInVM;

		// Money
		private double _maxMoney;
		private double _currentMoney;

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

		public int NumberMaxProductsByStorage
		{
			get { return _numberMaxProductsByStorage; }
			set 
			{
				if (value <= 0)
					throw VMExceptionUtils.CanHaveZeroOrLowerAsStorageMaxCapacity();

				_numberMaxProductsByStorage = value; 
			}
		}


		/// <summary>
		/// Tell if you have still more empty storage
		/// </summary>
		/// <value>The has empty storage.</value>
		public bool HasEmptyStorage
		{
			get { return (_numberOfReserve - _storageProducts.Count) > 0; }
		}

		#endregion

		#region Constructor

		public VendingMachine(int numberOfReserve)
		{
			Initialization();
			_numberOfReserve = numberOfReserve;
		}

		#endregion

		#region Initialization

		private void Initialization()
		{
			_numberOfProducts = 0;
			_numberOfReserve = 0;
			_numberOfProducts = 0;
			_storageProducts = new Dictionary<string, T>();

			_maxMoney = int.MaxValue; // max double will be humanly impossible.
			_listMoneysInVM = new StorageMoneyTuple();
			_currentMoney = 0;
		}

		#endregion

		#region Implements IVendingMachine

		public double MaxMoney
		{
			get
			{
				return _maxMoney;
			}

			set
			{
				if (value <= 0)
					throw VMExceptionUtils.MaxMoneyCantBeNegative();

				if (value < _currentMoney)
					throw VMExceptionUtils.MaxMoneyCantBeInfToCurrentMoney();

				_maxMoney = value;
			}
		}

		/// <summary>
		/// Number max of Products you can add in this Vending Machine
		/// </summary>
		/// <value>The number max products.</value>
		public int NumberMaxProducts
		{
			get
			{
				return _numberOfProducts;
			}
		}

		public int NumberOfProductsLeft
		{
			get
			{
				// TODO : continue this function
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

		public double CurrentMoneyClient
		{
			get { return _currentMoney; }
		}

		public bool StillMoneyLeft
		{
			get { return _currentMoney > 0; }
		}


		/// <summary>
		/// Add a product to a specific storage. 
		/// WARNING : You must had a storage first.
		/// </summary>
		/// <returns>Operation did or not</returns>
		/// <param name="idStorage">Identifier for the storage</param>
		/// <param name="product">Optional : a product</param>
		public bool AddProduct(string idStorage, Product product = null)
		{
			if (!IsAStorageIdAlreadyExists(idStorage))
				throw VMExceptionUtils.StorageDoesntExists();

			if (_storageProducts[idStorage].IsFull)
				throw VMExceptionUtils.StorageIsFull();

			_storageProducts[idStorage].AddOneProduct();
			_numberOfProducts++;

			return true;
		}


		/// <summary>
		/// Removes all products in every storage in he Vending Machine
		/// </summary>
		/// <returns>The all products.</returns>
		public void RemoveAllProducts()
		{
			_storageProducts.Values.ForEach((storage) => { storage.ClearProducts(); });
			_numberOfProducts = 0;
		}
		

		/// <summary>
		/// Remove a product on a specific storage.
		/// Exception ; Storage doesn't exist
		/// </summary>
		/// <returns>Return states operation true or false</returns>
		/// <param name="idStorage">Identifier storage.</param>
		/// <param name="product">Product.</param>
		public bool RemoveProduct(string idStorage, Product product = null)
		{
			if (!IsAStorageIdAlreadyExists(idStorage))
				throw VMExceptionUtils.StorageDoesntExists();

			_storageProducts[idStorage].RemoveOneProduct();
			_numberOfProducts--;

			return true;
		}


		/// <summary>
		/// Give the number of product available in a storage
		/// </summary>
		/// <returns>The number of product from storage.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public int GetNumberOfProductFromStorage(string idStorage)
		{
			if (!IsAStorageIdAlreadyExists(idStorage))
				throw VMExceptionUtils.StorageDoesntExists();

			return _storageProducts[idStorage].CurrentCapacity;
		}

		/// <summary>
		/// Get the list of all Id Storages available
		/// </summary>
		/// <returns>The storages identifiers.</returns>
		public IEnumerable<string> GetStoragesIds() => _storageProducts.Select(s => s.Value.IdStorage).ToList();

		public void ResetMoneyClientHasSpent()
		{
			throw new NotImplementedException();
		}

		public void ClientPutMoney(Money m)
		{
			IsThisMoneyAuthorized(m).IfFalseThrow(VMExceptionUtils.MoneyNotAuthorized);
			// TODO : Verify max money

			// we store the money, if the client ask their money back, they will have random moneys.
			_listMoneysInVM[m] = _listMoneysInVM[m] + 1;

			// We update Client Money
			_currentMoney += m.Value;
		}


		/// <summary>
		/// Give back the money to the client and remove the neccessary coins to the Client
		/// </summary>
		/// <returns>The money back from vm.</returns>
		public RestOfMoney GetMoneyBackFromVM()
		{
			if (_currentMoney <= 0) return new RestOfMoney();

			var lists = GetListMoneyFromClientMoney();

			// We remove the money from our storage
			lists.ForEach((tuple) => { RemoveMoney(tuple.Item2, tuple.Item1); });

			// We put our current money to zero
			_currentMoney = 0;

			return lists;
		}

		/// <summary>
		/// Get the total amount of the machine by making a summ of all the moneys stored.
		/// </summary>
		/// <returns>Total Money</returns>
		public double GetTotalMoneyMachine() => _listMoneysInVM.Sum((keyValM) => keyValM.Key.Value * keyValM.Value);

		/// <summary>
		/// Retrieve the list of accepted money by the Vending Machine
		/// </summary>
		/// <returns>The list authorized money.</returns>
		public List<Money> GetListAuthorizedMoney() => _listMoneysInVM.Keys.ToList();

		/// <summary>
		/// Set the new list of money that the veding machine can take
		/// </summary>
		/// <param name="moneyList">New List of money</param>
		public void SetAuthorizeMoneyList(IEnumerable<Money> moneyList)
		{
			(moneyList.IsNotNullOrEmpty()).IfFalseThrow<ArgumentException>("Money list is empty.");
			//_listAuthorizedMoney = moneyList.ToList();

			// TODO : should continue this function
			throw new NotImplementedException();
		}

		/// <summary>
		/// Add new money to the accept list of the vending machine
		/// </summary>
		/// <param name="money">Money.</param>
		public void AddMoneyAuthorizedMoney(Money money)
		{
			// We add the money if the list doesn't contain the money yet
			(_listMoneysInVM.ContainsKey(money)).IfFalse(() => _listMoneysInVM.Add(money, 0));
		}

		/// <summary>
		/// Add to accept moneys list all Money.
		/// </summary>
		/// <param name="moneyArgs">Money(s) arguments.</param>
		public void AddMoneyAuthorizedMoney(params Money[] moneyArgs)
		{
			(moneyArgs.IsNotNullOrEmpty()).IfFalseThrow<ArgumentException>("moneyArgs parameter => can be null or empty");
			moneyArgs.ForEach(AddMoneyAuthorizedMoney);
		}

		/// <summary>
		/// Remove this money from the acceptlist
		/// </summary>
		/// <param name="money">Money.</param>
		public void RemoveAuthorizedMoney(Money money)
		{
			((_listMoneysInVM.ContainsKey(money))).IfTrue(() => _listMoneysInVM.Remove(money));
		}

		/// <summary>
		/// Remove to accept moneys all the Money
		/// </summary>
		/// <param name="moneyArgs">Money(s) to remove</param>
		public void RemoveAuthorizedMoney(params Money[] moneyArgs)
		{
			(moneyArgs.IsNotNullOrEmpty()).IfFalseThrow<ArgumentException>("moneyArgs parameter => can be null or empty");
			moneyArgs.ForEach(RemoveAuthorizedMoney);
		}

		/// <summary>
		/// Check if 
		/// </summary>
		/// <returns><c>true</c>, The vending machine accept this money, <c>false</c> The vending machine doesn't accept this money.</returns>
		/// <param name="money">Money.</param>
		public bool IsThisMoneyAuthorized(Money money) => _listMoneysInVM.ContainsKey(money);

		/// <summary>
		/// Retrieve the product 
		/// </summary>
		/// <returns>The get product.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public VMErrorCode CanGetProduct(string idStorage)
		{
			VMErrorCode code = VMErrorCode.NONE;
			IsAStorageIdAlreadyExists(idStorage).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			(_storageProducts[idStorage].IsEmpty)
				.IfTrue(() => { code = VMErrorCode.NO_MORE_PRODUCTS; })
				.IfFalse(() => { code = VMErrorCode.CAN_HAVE_PRODUCT; });

			return code;
		}


		/// <summary>
		/// Gets the product.
		/// </summary>
		/// <returns>The product.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public VMErrorCode GetProduct(string idStorage)
		{
			var result = CanGetProduct(idStorage);

			// We remove a product from the store
			(result == VMErrorCode.CAN_HAVE_PRODUCT).IfTrue(() => _storageProducts[idStorage].RemoveOneProduct());

			return result;
		}

		#endregion

		#region Storage


		/// <summary>
		/// Check if a container ID already exists
		/// </summary>
		/// <returns>The AS torage identifier already exists.</returns>
		/// <param name="id">Identifier.</param>
		public bool IsAStorageIdAlreadyExists(string id) => _storageProducts.ContainsKey(id);


		/// <summary>
		/// Create new Storage if there still place.
		/// Exception : no more storage empty available.
		/// Exception : Max capacity per storage was not define
		/// Exception : Storage Id already exists
		/// </summary>
		/// <returns>The new storage of type T</returns>
		/// <param name="id">Identifier of the new Storage</param>
		public T CreateNewStorage(string id)
		{
			(!HasEmptyStorage).IfTrue(() => { throw VMExceptionUtils.NoMoreReserve(); });
			(_numberMaxProductsByStorage <= 0).IfTrue(() => { throw VMExceptionUtils.CanHaveZeroOrLowerAsStorageMaxCapacity(); });
			(IsAStorageIdAlreadyExists(id)).IfTrue(() => { throw VMExceptionUtils.StorageAlreadyExists(); });

			// Error : an erro may occur here if instance don't have the good instance
			var storage = StorageFactory.Instance.CreateInstance<T>(new object[] { id, _numberMaxProductsByStorage });

			// We add our new storage in our self container
			var res = (T)storage;
			_storageProducts.Add(id, res);

			return res;
		}

		/// <summary>
		/// Create a new storage and set the price in the same time.
		/// Exception : no more storage empty available.
		/// Exception : Max capacity per storage was not define
		/// Exception : Storage Id already exists
		/// </summary>
		/// <returns>The new storage.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		/// <param name="price">Price by product</param>
		public T CreateNewStorage(string idStorage, double price)
		{
			// An exception will occur here, so just let him like that, no need to catch
			var newStorage = CreateNewStorage(idStorage);
			newStorage.Price = price;

			return newStorage;
		}

		/// <summary>
		/// Set price for a storage
		/// </summary>
		/// <returns>The price storage.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		/// <param name="price">Price.</param>
		public void SetPriceStorage(string idStorage, double price)
		{
			(IsAStorageIdAlreadyExists(idStorage)).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			_storageProducts[idStorage].Price = price;
		}

		/// <summary>
		/// Get the price fix on a storage
		/// </summary>
		/// <returns>The price product on this storage.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public double GetPriceProductOnThisStorage(string idStorage)
		{
			(IsAStorageIdAlreadyExists(idStorage)).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			return _storageProducts[idStorage].Price;
		}

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

			var newStorage = CreateNewStorage(newIndexStorage);
			return newStorage.ToMaybe();
		}

		/// <summary>
		/// Retrieve if you can place in one of the Storage
		/// </summary>
		/// <returns>The have place on AS torage.</returns>
		/// <param name="id">Identifier.</param>
		public bool StillHavePlaceOnAStorage(string id)
		{
			(IsAStorageIdAlreadyExists(id)).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			return ! _storageProducts[id].IsFull;
		}

		/// <summary>
		/// Remove a storage
		/// </summary>
		/// <returns>The storage.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public void RemoveStorage(string idStorage)
		{
			(IsAStorageIdAlreadyExists(idStorage)).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			_storageProducts.Remove(idStorage);
		}

		/// <summary>
		/// Check if the storage is empty or not
		/// </summary>
		/// <returns>The this storage is empty.</returns>
		/// <param name="idStorage">Identifier storage.</param>
		public bool IsThisStorageIsEmpty(string idStorage)
		{
			(IsAStorageIdAlreadyExists(idStorage)).IfFalseThrow(VMExceptionUtils.StorageDoesntExists);
			return _storageProducts[idStorage].IsEmpty;
		}

		/// <summary>
		/// Get the current state of the storage right now
		/// </summary>
		/// <returns>The storage.</returns>
		public Dictionary<string, int> StatesStorage() => _storageProducts.ToDictionary(dicEn => dicEn.Key, dicEn => dicEn.Value.NumberProducts);

		#endregion


		#region Money


		/// <summary>
		/// Gets the list money from client money. Give back by the geater value that I have
		/// You must change this algorithm if you want giving back money differently. 
		/// Bastien : I know some Vending Machine will give back coins first. It's depend how to you will back the money
		/// </summary>
		/// <returns>The list money from client money.</returns>
		private List<SeveralMoney> GetListMoneyFromClientMoney()
		{
			var restMoneyClient = _currentMoney;
			var orderList = _listMoneysInVM.OrderByDescending(p => p.Key.Value ).Select( p => p );

			var res = new List<SeveralMoney>();

			var unitNeeded = 0;
			var tmpReduceFromRest = 0d;

			// TODO : imporve the foreach
			orderList.ForEach<Money, int>( (money, unitHave) =>
			{

				if (restMoneyClient >= money.Value)
				{
					unitNeeded = Convert.ToInt32(Math.Floor(restMoneyClient / money.Value));
					tmpReduceFromRest = (unitNeeded > unitHave) ? (money.Value * unitHave) : (money.Value * unitNeeded);

					// We remove the good amount
					restMoneyClient -= tmpReduceFromRest;

					(unitNeeded > 0).IfTrue( () => res.Add( new SeveralMoney(unitNeeded, money) ) ); 
				}
			});

			(restMoneyClient > 0).IfTrueThrow<ApplicationException>("Impossible. You should have the same at least the amount you put");

			return res;
		}

		/// <summary>
		/// Clear the money into the Vending Machine.
		/// </summary>
		private void ClearMoneyIntheMachine() => _listMoneysInVM.ForEach<Money, int>((key, val) => { _listMoneysInVM[key] = 0; });


		/// <summary>
		/// We fill the Vending Machine with moneys
		/// </summary>
		/// <param name="money">Money.</param>
		/// <param name="units">Units of money you want to add</param>
		public void FillMoney( Money money, int units )
		{
			_listMoneysInVM.ContainsKey(money).IfFalseThrow(VMExceptionUtils.MoneyNotAuthorized);
			_listMoneysInVM[money] += units;
		}

		public void FillMoney(IDictionary<Money, int> credits) => credits.ForEach((keyVal) => FillMoney(keyVal.Key, keyVal.Value));

		public void RemoveMoney(Money money, int units)
		{
			_listMoneysInVM.ContainsKey(money).IfFalseThrow(VMExceptionUtils.MoneyNotAuthorized);
			(_listMoneysInVM[money] < units).IfTrueThrow( VMExceptionUtils.NotEnoughMoneyInVendingMachine);
			_listMoneysInVM[money] -= units;
		}

		#endregion
	}
}

