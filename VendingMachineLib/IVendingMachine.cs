using System;
using System.Collections.Generic;

namespace Com.Bvinh.Vendingmachine
{
	using Com.Bvinh.Vendingmachine.Exceptions;

	// That will help us to understand the definition of the program
	// Up to you use or not
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
		double CurrentMoneyClient { get; }
		bool StillMoneyLeft { get; }

		double GetTotalMoneyMachine();
		void RemoveAllProducts();
		List<Money> GetListAuthorizedMoney();


		// Products
		VMErrorCode CanGetProduct(string idStorage);
		VMErrorCode GetProduct(string idStorage);

		// Money management
		void SetAuthorizeMoneyList(IEnumerable<Money> moneyList);
		void AddMoneyAuthorizedMoney(params Money[] moneyArgs);
		void AddMoneyAuthorizedMoney(Money money);
		void RemoveAuthorizedMoney(params Money[] moneyArgs);
		void RemoveAuthorizedMoney(Money money);
		bool IsThisMoneyAuthorized(Money money);


		IEnumerable<string> GetStoragesIds();


		// TODO : Explain the int IdStorage
		bool RemoveProduct(string idStorage, Product product = null);
		bool AddProduct(string idStorage, Product product = null);

		void ResetMoneyClientHasSpent();
		int GetNumberOfProductFromStorage(string idStorage);


		void ClientPutMoney(Money m);
		RestOfMoney GetMoneyBackFromVM();

	}
}

