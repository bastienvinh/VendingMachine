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
		void RemoveAllProducts();
		List<Money> GetListAuthorizedMoney();
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
		void GiveBackClientMoney();

		int GetNumberOfProductFromStorage(string idStorage);


		void ClientPutMoney(Money m);
		RestOfMoney GetMoneyBackFromVM();

	}
}

