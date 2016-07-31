using System;
using System.Collections.Generic;


namespace Com.Bvinh.Vendingmachine
{

	// That will help us to understand the definition of the program
	// Up to you use or not
	using SeveralMoney = Tuple<int, Money>;
	using RestOfMoney = List<Tuple<int, Money>>;


	/// <summary>
	/// Vending machine rules, this is what we expect from vending machine
	/// </summary>
	public interface IVendingMachine
	{

	  double MaxMoney { get; set; } 
		int NumberMaxProducts { get; }
		int NumberOfProductsLeft { get; }
		bool HaveProducts { get; }

		double GetTotalMoneyClient();
		double GetTotalMoneyMachine();
		double GetCurrentClientMoney();
		double RemoveAllProducts();


		// TODO : Explain the int IdStorage
		bool RemoveProduct(Product product, int IdStorage);
		bool AddProduct(Product product, int IdStorage);

		void ResetMoneyClientHasSpent();
		void GiveBackClientMoney();

		int GetNumberOfProductFromStorage(int idStorage);


		void ClientPutMoney(Money m);
		RestOfMoney GetMoneyBackFromVM();

	}
}

