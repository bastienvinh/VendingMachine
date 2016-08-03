using System;
using System.Linq;
using System.Collections.Generic;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Linq;


namespace Com.Bvinh.Test.Vending
{
	class MainClass
	{

		private static SoftDrinksMachine SelfSoftDrinkVM = new SoftDrinksMachine();

		// This space is used to test directly some function manually.

		public static void Main(string[] args)
		{
			Init();

			// We fill tha machine, the capacity is 30 cokes, 20 sprites, 30 fanta and 20 juices

			Console.WriteLine("\n\nEnd of Program guys ...............................");
		}


		private static void Init()
		{
			SelfSoftDrinkVM = new SoftDrinksMachine();

			// We fill the vending machine
			Xfb.Range(30).ForEach((i) => { SelfSoftDrinkVM.AddMoreCoke(); });
			Xfb.Range(20).ForEach((i) => { SelfSoftDrinkVM.AddMoreSprite(); });
			Xfb.Range(30).ForEach((i) => { SelfSoftDrinkVM.AddMoreFanta(); });
			Xfb.Range(20).ForEach((i) => { SelfSoftDrinkVM.AddMoreJuice(); });
		}
	}
}
