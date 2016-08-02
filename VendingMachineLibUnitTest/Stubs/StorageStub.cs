using System;
using Com.Bvinh.Vendingmachine;

namespace VendingMachineLibUnitTest
{
	public class StorageStub : IStorageVMProducts
	{
		#region Properties
		public int CurrentCapacity
		{
			get
			{
				return default(int);
			}
		}

		public string IdStorage
		{
			get
			{
				return default(string);
			}
		}

		public bool IsFull
		{
			get
			{
				return default(bool);
			}
		}

		public int MaxCapacity
		{
			get
			{
				return default(int);
			}
		}
		#endregion

		#region Methods
		public void AddOneProduct() { }
		public void AddOneProduct(Product aProduct) { }
		public void ClearProducts() { }
		public void ClearProducts(TypeOfProduct type) { }
		public void RemoveAProduct(Product aProducts) { }
		public void RemoveOneProduct() { }
		public void SetCapacity(int number) { }
		#endregion
	}
}

