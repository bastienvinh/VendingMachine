using System;
using Com.Bvinh.Vendingmachine;


namespace Com.Bvinh.Test.Vending
{
	class MainClass
	{

		// This space is used to test directly some function manually.

		public static void Main(string[] args)
		{
			// We declare our Vending machine first
			var vendingMachine = new VendingMachine<OldFashionStorageVM>(5);
			vendingMachine.NumberMaxProductsByStorage = 10;

			// Each machine has a combinaison
			var test = vendingMachine.CreateNewStorage("C1");
			test.GetHashCode();


			Console.WriteLine("\n\nEnd of Program guys ...............................");
		}
	}
}
