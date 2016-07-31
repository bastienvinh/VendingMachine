using System;
using Functional.Maybe;
using System.Reflection;
using System.Linq;

namespace Com.Bvinh.Vendingmachine
{

	// Bastien :  I will create Money as Enumeration Pattern because I consider a money value is unique and doesn't need instance for this.
	// Money is just Universal Value

	// Code :  Something uniq to identify the money
	// Value :  can be the same
	// Name : it's up to you

	// The type of money never change once every 10 years or more.
	// For decade we use 1,2,5, 10, ... os if you had any value you can here your value.

	// For now I tale in account the currency since a vending machine doesn't manage multiple currencies


	public sealed class Money
	{
		#region Properties

		public int Code { get; private set; }
		public double Value { get; private set; }
		public string ReferenceType { get; private set; }
		public string Name { get; }

		#endregion


		#region Constructor

		private Money(int code, double value, string name, string referenceType)
		{
			Code = code;
			Value = value;
			Name = name;
			ReferenceType = referenceType;
		}

		#endregion


		#region Enumeration Types

		public static Money P1 = new Money(1, 1, "1p", "p1");
		public static Money P2 = new Money(2, 2, "2p", "p2");
		public static Money P3 = new Money(3, 5, "5p", "p5");
		public static Money P10 = new Money(4, 10, "10p", "p10");
		public static Money P20 = new Money(5, 20, "20p", "p20");
		public static Money P50 = new Money(6, 50, "50p", "p50");

		#endregion

		#region Static Utils

		// WARNING : Don't modify this function if you don't know what you do. It work for any case.

		public static Maybe<Money> GetMoneyByValue(double value)
		{

			// Money is always positive
			if (value < 0)
				throw new MoneyException("You can have money value inferior to 0");

			var searchElement = typeof(Money).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.Static)
															.FirstOrDefault(f => f.FieldType == typeof(Money)
																							&& ((Money)f.GetValue(null)).Value == value);


			return (searchElement != null) ? ((Money) searchElement.GetValue(null)).ToMaybe() : Maybe<Money>.Nothing;
		}

		#endregion
	}
}

