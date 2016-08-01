using System;
namespace Com.Bvinh.Vendingmachine
{
	/// <summary>
	/// Drink Product. It can be softdrink, bottle, ...
	/// </summary>
	public class DrinksProduct : Product
	{
		// I based my response from water we can find on a pool.
		// If you can sell a pool on a vending machine and let people brings easily the pool like nothing (call me we make business :p)
		private const float MAX_LITRES_POSSIBLE = 69741;

		#region Attributes
		// TODO : Add type of drinks
		private float _litres;
		#endregion

		#region Properties
		public float Litres
		{ 
			get
			{
				return _litres;
			}
			set
			{
				if (value <= 0 || value > MAX_LITRES_POSSIBLE)
					throw new ProductException(string.Format("You can have only a drink between the size > 0 and < {0}", 
					                                         MAX_LITRES_POSSIBLE));

				_litres = value;
			}
		}
		#endregion
	}
}

