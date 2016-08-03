using System;
namespace Com.Bvinh.Vendingmachine
{

	// REMARK : Why it is deisgned this way ...
	// REMARK : IStorageVMProducts is generic interface to use in almost all case because we don't how Vending Machine could react.
	// REMARK : Since we implement an old fashion Vending Machine, 
	//          we don't need the product. Because in real-life, they can put multiple product on the same storage.
	//          What is important is the capacity and the price in our case. 



	/// <summary>
	/// The Old fashion Storage speak of himself. That means a fix storage and multiple on a Vending Machine.
	/// </summary>
	public class OldFashionStorageVM : IStorageVMProducts, IStoragePriceVMProducts
	{

		#region Constants
		private const int CAPACITY_STORAGE_NUMBERS_IS_NONE = 0;
		#endregion

		#region Attributes
		private int _capacityMax;
		private int _numberProductsOnStorage;
		private string _idStorage;
		private double _price;
		#endregion


		#region Properties

		public double Price
		{
			get { return _price; }
			set 
			{
				if (value < 0)
					throw new StorageException("The price can't be negative");

				_price = value;
			}
		}

		#endregion

		#region Constructor
		public OldFashionStorageVM(int maxCapacity)
		{
			Initialisation();
			SetCapacity(maxCapacity);
		}

		public OldFashionStorageVM(string id, int maxCapacity)
			: this(maxCapacity)
		{

			if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
				throw new StorageException("IdStorage can't be null, empty or with whitespace.");

			_idStorage = id;
		}
		#endregion

		#region Implements IStorageVMProducts

		/// <summary>
		/// Give back the current capcity available (max - numberOfProduct already stored)
		/// </summary>
		/// <value>The current capacity.</value>
		public int CurrentCapacity
		{
			get
			{
				return _capacityMax - _numberProductsOnStorage;
			}
		}


		/// <summary>
		/// Give back the current Storage
		/// </summary>
		/// <value>The identifier storage.</value>
		public string IdStorage
		{
			get
			{
				return _idStorage;
			}
		}

		/// <summary>
		/// Give back the current max capacity
		/// </summary>
		/// <value>The max capacity.</value>
		public int MaxCapacity
		{
			get
			{
				return _capacityMax;
			}
		}

		/// <summary>
		/// Tell if the storage is currently full and can't add more products on it
		/// </summary>
		/// <value>The is full.</value>
		public bool IsFull
		{
			get { return _numberProductsOnStorage >= _capacityMax; }
		}

		/// <summary>
		/// Tell if the storage is empty or not
		/// </summary>
		/// <value>This is empty or not</value>
		public bool IsEmpty
		{
			get
			{
				return _numberProductsOnStorage <= 0; 
			}
		}

		/// <summary>
		/// Get the number of products stored
		/// </summary>
		/// <value>The number products.</value>
		public int NumberProducts
		{
			get
			{
				return _numberProductsOnStorage;
			}
		}

		/// <summary>
		/// Increment of one product in the current capacity
		/// </summary>
		/// <returns>The one product.</returns>
		public void AddOneProduct()
		{
			if (_numberProductsOnStorage >= _capacityMax)
				throw new StorageException("You can't add more on this storage.");

			++_numberProductsOnStorage;
		}

		public void AddOneProduct(Product aProduct)
		{
			// We don't care about the product here
			AddOneProduct();
		}

		/// <summary>
		/// Remove all products in the current capacity / number = 0
		/// </summary>
		/// <returns>The products.</returns>
		public void ClearProducts()
		{
			_numberProductsOnStorage = 0;
		}

		public void ClearProducts(TypeOfProduct type)
		{
			// We still don't care about the product here
			ClearProducts();
		}

		public void RemoveAProduct(Product aProducts)
		{
			// We don't really care about the product
			RemoveOneProduct();
		}

		/// <summary>
		/// Remove one product in our storage
		/// </summary>
		/// <returns>The one product.</returns>
		public void RemoveOneProduct()
		{
			if (_numberProductsOnStorage > 0)
				--_numberProductsOnStorage;
		}

		/// <summary>
		/// We modify the current maximum capacity
		/// </summary>
		/// <returns>The capacity.</returns>
		/// <param name="number">Number.</param>
		public void SetCapacity(int number)
		{
			if (number <= CAPACITY_STORAGE_NUMBERS_IS_NONE)
				throw new StorageException("Your storage must have a capacity > 0.");

			_capacityMax = number;
		}

		#endregion


		#region Utils

		private void Initialisation()
		{
			_capacityMax = 0;
			_numberProductsOnStorage = 0;
			_idStorage = SomeUtilsMethods.CreateUniqueIdentifier();
			_price = 0;
		}

		#endregion
	}
}

