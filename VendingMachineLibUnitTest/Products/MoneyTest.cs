using System;
using NUnit.Framework;
using NUnit;

using Com.Bvinh.Vendingmachine; 

namespace VendingMachineLibUnitTest
{
	[TestFixture]
	public class MoneyTest
	{
		
		public MoneyTest()
		{
		}



		[Description("Test if we have the good value when we're looking for an existing value")]
		[Test]
		public void TestWeHaveMoney()
		{
			var res =  Money.GetMoneyByValue(2);
			Assert.IsTrue(res.HasValue);
			Assert.AreEqual(res.Value.Value, 2);
			Assert.AreEqual(res.Value, Money.P2);
			Assert.AreSame(res.Value, Money.P2);
			Assert.AreEqual(res.Value.Name, Money.P2.Name);
			Assert.AreEqual(res.Value.ReferenceType, Money.P2.ReferenceType);
			Assert.AreEqual(res.Value.Code, Money.P2.Code);
			Assert.AreEqual(res.Value.Value, Money.P2.Value);
		}


		[Description("Test with value has no value at all")]
		[Test]
		public void TestWeDontHaveMoneyForOurValue()
		{
			var res = Money.GetMoneyByValue(9999);
			Assert.IsFalse(res.HasValue);
		}

		public void TestMoneyMustBePositive()
		{
			var res = Money.GetMoneyByValue(-2);
		}

	}
}

