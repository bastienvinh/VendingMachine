using System;
namespace Com.Bvinh.Vendingmachine
{
	/// <summary>
	/// Abstract class for any type of products
	/// </summary>
	public abstract class Product
	{

		#region Attributes
		protected double _price;
		protected float _directTax;
		#endregion

		#region Properties

		public string Name { get; set; }

		public float DirectTax
		{
			get { return _directTax; }
			set
			{
				if (value < 0 || value > 1)
					throw new ProductException("Direct tax can be only between 0 and 1");

				_directTax = value;
			}
		}

		public double Price
		{
			get { return _price; }
			set 
			{
				if (value < 0)
					throw new ProductException("Price is always positive");

				_price = value;
			}
		}

		#endregion
	}

	/// <summary>
	/// Asny product you want
	/// </summary>
	public class AnyProduct : Product
	{
	}
}

