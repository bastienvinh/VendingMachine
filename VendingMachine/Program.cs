using System;
using System.Linq;
using System.Collections.Generic;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Linq;
using System.Reflection;

namespace Com.Bvinh.Test.Vending
{
	class MainClass
	{

		private static SoftDrinksMachineSimulation SelfSoftDrinkVM;

		// This space is used to test directly some function manually.

		public static void Main(string[] args)
		{
			Init();


			Console.WriteLine("Absolute of 1.5 : {0}", Math.Abs(1.5d));

			//SelfSoftDrinkVM.ShowFullStorage();
			//// Test of our libs
			//TestAScenario1();

			// We fill tha machine, the capacity is 30 cokes, 20 sprites, 30 fanta and 20 juices

			Console.WriteLine("\n\nEnd of Program guys ...............................");
		}

		private static void Init()
		{

			SelfSoftDrinkVM = new SoftDrinksMachineSimulation();

			// We fill the vending machine
			SelfSoftDrinkVM.AddAllDrinks();
			SelfSoftDrinkVM.FillStartMoney();
		}


		private static void TestAScenario1()
		{
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();
			SelfSoftDrinkVM.ClientPutSomeMoney();
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();
			SelfSoftDrinkVM.ShowHowManyTheClientSpentForNow();
			SelfSoftDrinkVM.CancelAndGetBackMoney();
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();

			SelfSoftDrinkVM.BuyAllSprite();

			Console.WriteLine("After all the sprite were bought");
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();

		}
	}
}
