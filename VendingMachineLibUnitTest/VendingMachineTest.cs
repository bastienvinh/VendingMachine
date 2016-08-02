using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Vendingmachine.Utils;
using Moq;

namespace VendingMachineLibUnitTest
{
	[TestFixture]
	public class VendingMachineTest
	{
		
		[Description("Test about the vending has empty storage or not")]
		[Test]
		public void TestToHaveEmptyStorage()
		{
			const int LOCAL_CONST_MAX_STORAGE = 10;
			const string LOCAL_CONST_ERROR_MESSAGE = "No more storage available in the current Vending Machine";


			var mock = new Mock<IFactory>();

			mock.Setup(m => m.CreateInstance<StorageStub>()).Returns(It.IsAny<StorageStub>());

			var vendingMachine = new VendingMachine<StorageStub>(LOCAL_CONST_MAX_STORAGE);
			vendingMachine.NumberMaxProductsByStorage = 10;

			// Vending Machine has always empty storage at the start 
			Assert.IsTrue(vendingMachine.HasEmptyStorage);

			Assert.DoesNotThrow(() =>
			{
				Enumerable.Range(0, LOCAL_CONST_MAX_STORAGE)
				          .ForEach((i) => { vendingMachine.CreateNewStorage(i.ToString()); });
			});


			Assert.Throws<VMSupplierProductTypeException>(() => {
				vendingMachine.CreateNewStorage("test");
			}, LOCAL_CONST_ERROR_MESSAGE);

			Assert.IsFalse(vendingMachine.HasEmptyStorage);
		}

	}
}

