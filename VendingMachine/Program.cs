using System;
using Com.Bvinh.Vendingmachine;


namespace Com.Bvinh.Test.Vending
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var possibleMaybeMoney = Money.GetMoneyByValue(2);
			if (possibleMaybeMoney.HasValue)
			{
				Console.WriteLine("Current Money retrieve : {0}", possibleMaybeMoney.Value.Name);
			}
			else
			{
				Console.WriteLine("No money at all");
			}

			Console.WriteLine("\n\nEnd of Program guys ...............................");
		}
	}
}
