using System;
using System.Collections.Generic;

namespace Com.Bvinh.Vendingmachine
{

	// We add an interface to store storage type of Products
	// You have two type of vending machine, a olf one and a new one with better interface and management
	// A old one will have multiple Storage, when a new one can have one general storage.
	// So we need this interface to remove a product on a storage 


	// NAME : VM => vending machine


	/// <summary>
	/// Interface to manage different kind of stocks, because our products must be store somewhere
	/// </summary>
	public interface IStorageVMProducts
	{

		string IdStorage { get; }
		int MaxCapacity { get; }
		int CurrentCapacity { get; }
		bool IsFull { get; }

		void SetCapacity(int number);
		void AddOneProduct();
		void RemoveOneProduct();
		void AddOneProduct(Product aProduct);
		void RemoveAProduct(Product aProducts);
		void ClearProducts();
		void ClearProducts(TypeOfProduct type);
	}
}

