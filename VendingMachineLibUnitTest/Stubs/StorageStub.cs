using System;
using Com.Bvinh.Vendingmachine;

namespace VendingMachineLibUnitTest
{
	public abstract class StorageStub : IStorageVMProducts, IStoragePriceVMProducts
	{
		#region Properties
		public int CurrentCapacity { get; set; }
		public string IdStorage { get; set; }
		public bool IsEmpty { get; set; }
		public bool IsFull { get; set; }
		public int MaxCapacity { get; set; }
		public int NumberProducts { get; set; }
		public double Price { get; set; }
		#endregion

		#region Methods
		public virtual void AddOneProduct() { }
		public virtual void AddOneProduct(Product aProduct) { }
		public virtual void ClearProducts() { }
		public virtual void ClearProducts(TypeOfProduct type) { }
		public virtual void RemoveAProduct(Product aProducts) { }
		public virtual void RemoveOneProduct() { }
		public virtual void SetCapacity(int number) { }
		#endregion
	}
}

