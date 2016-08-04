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

			// Test of our libs
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();
			SelfSoftDrinkVM.ClientPutSomeMoney();
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();
			SelfSoftDrinkVM.ShowHowManyTheClientSpentForNow();
			SelfSoftDrinkVM.CancelAndGetBackMoney();
			SelfSoftDrinkVM.ShowOnConsoleTotalOnMachine();

			// We fill tha machine, the capacity is 30 cokes, 20 sprites, 30 fanta and 20 juices

			Console.WriteLine("\n\nEnd of Program guys ...............................");
		}

		private static void Init()
		{

			SelfSoftDrinkVM = new SoftDrinksMachineSimulation();

			// We fill the vending machine
			Xfb.Range(30).ForEach((i) => { SelfSoftDrinkVM.AddMoreCoke(); });
			Xfb.Range(20).ForEach((i) => { SelfSoftDrinkVM.AddMoreSprite(); });
			Xfb.Range(30).ForEach((i) => { SelfSoftDrinkVM.AddMoreFanta(); });
			Xfb.Range(20).ForEach((i) => { SelfSoftDrinkVM.AddMoreJuice(); });

			SelfSoftDrinkVM.FillStartMoney();
		}
	}
}
