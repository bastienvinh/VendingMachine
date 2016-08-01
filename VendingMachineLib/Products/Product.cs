using System;
namespace Com.Bvinh.Vendingmachine
{
	public class Product
	{

		#region Attributes
		private double _price;
		private float _directTax;
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

		#endregion



	}
}

