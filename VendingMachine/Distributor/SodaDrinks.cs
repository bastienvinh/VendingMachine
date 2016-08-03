using System;
using Com.Bvinh.Vendingmachine;

namespace Com.Bvinh.Test.Vending
{
	// Bastien : this an example of the program, so it is not generic at all
	
	public sealed class SodaCanDrinks : DrinksProduct
	{
		#region Enumeration
		public static readonly SodaCanDrinks CokeCanDrink = new SodaCanDrinks("Coca", 13);
		public static readonly SodaCanDrinks FantaCanDrink = new SodaCanDrinks("Fanta", 15);
		public static readonly SodaCanDrinks SpriteCanDrinks = new SodaCanDrinks("Sprite", 0);
		public static readonly SodaCanDrinks JuiceCanDrinks = new SodaCanDrinks("Juice", 24);

		#endregion

		private SodaCanDrinks( string name, double price )
		{
			_price = price;
			Name = name;
			_litres = DrinksProduct.DEFAULT_SIZE_CAN;
		}

	}

}

