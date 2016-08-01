using System;
using NUnit;
using NUnit.Framework;
using Com.Bvinh.Vendingmachine;
using Moq;

namespace VendingMachineLibUnitTest
{
	public static class SystemTime
	{
		public static Func<DateTime> Now = () => new DateTime(2015, 12, 05);
	}

	[TestFixture]
	public class SomeUtilsMethodsTest
	{

		[Description("You can have an empty string by generating the new stringFromDate method.")]
		[Test]
		public void CreateCreateStringFromDateIsNotNullOrEmpty()
		{
			var res = SomeUtilsMethods.CreateCreateStringFromDate();
			Assert.IsNotNull(res);
			Assert.IsNotEmpty(res);
		}

		// TODO : find a way to test the datetime.now with any framework

		//[Description("Generated a string unique key from a date with current format yyyyMMddhhmmss")]
		//[Test]
		//public void CreateCreateStringFromDateIsCorrect()
		//{
		//	var res = SomeUtilsMethods.CreateCreateStringFromDate();
		//	Console.WriteLine(DateTime.Now);
		//	Console.WriteLine(DateTime.Now);
		//	Console.WriteLine(DateTime.Now);
		//	res.GetHashCode();
		//}


	}
}

