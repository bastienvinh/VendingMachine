using System;
namespace Com.Bvinh.Vendingmachine
{
	/// <summary>
	/// Vending machine rules, this is what we expect from vending machine
	/// </summary>
	public interface IVendingMachine
	{

	  double MaxMoney { get; set; } 
		int NumberMaxProducts { get; }
		int NumberOfProductsLeft { get; }

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
		//void SetMaMoney(double maxMoney);
		//double GetMaxMoney();
	}
}

