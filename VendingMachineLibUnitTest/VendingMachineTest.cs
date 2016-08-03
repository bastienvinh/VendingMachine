using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Vendingmachine.Exceptions;
using Com.Bvinh.Linq;
using Moq;

namespace VendingMachineLibUnitTest
{
	[TestFixture]
	public class VendingMachineTest
	{

		private const int CLASS_VAR_MAX_STORAGE = 10;
		private const string CLASS_VAR_DEFAULT_STORAGE_ID = "Alibaba";

		
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


		[Description("We verified we create a new storage and can verify that exists in the Vending Machine")]
		[Test]
		public void TestIdAStorageExists()
		{
			const string LOCAL_VAR_FAKE_STORAGE_ID = "Millhouse";

			var mock = new Mock<IFactory>();
			mock.Setup(m => m.CreateInstance<StorageStub>()).Returns(It.IsAny<StorageStub>());

			var vendingMachine = new VendingMachine<StorageStub>(CLASS_VAR_MAX_STORAGE);
			vendingMachine.NumberMaxProductsByStorage = 20;

			vendingMachine.CreateNewStorage(LOCAL_VAR_FAKE_STORAGE_ID);

			Assert.IsTrue(vendingMachine.IsAStorageIdAlreadyExists(LOCAL_VAR_FAKE_STORAGE_ID));

		}

		[Description("A storage shouldn't exists at all.")]
		[Test]
		public void TestIdStorageShouldntExists()
		{
			const string LOCAL_VAR_FAKE_STORAGE_ID = "MollyJaba";

			var mock = new Mock<IFactory>();
			mock.Setup(m => m.CreateInstance<StorageStub>()).Returns(It.IsAny<StorageStub>());


			var vendingMachine = new VendingMachine<StorageStub>(CLASS_VAR_MAX_STORAGE);
			vendingMachine.NumberMaxProductsByStorage = 15;

			Assert.IsFalse(vendingMachine.IsAStorageIdAlreadyExists(LOCAL_VAR_FAKE_STORAGE_ID));
		}


		[Description("Remove a storage by ID in the Vending Machine")]
		[Test]
		public void TestToRemoveAStorageByID()
		{
			const string LOCAL_VAR_FAKE_STORAGE_ID = "MartianBar";

			var mock = new Mock<IFactory>();
			mock.Setup(m => m.CreateInstance<StorageStub>()).Returns(It.IsAny<StorageStub>());

			var vendingMachine = new VendingMachine<StorageStub>(CLASS_VAR_MAX_STORAGE);
			vendingMachine.NumberMaxProductsByStorage = 15;

			vendingMachine.CreateNewStorage(LOCAL_VAR_FAKE_STORAGE_ID);
			vendingMachine.RemoveStorage(LOCAL_VAR_FAKE_STORAGE_ID);

			Assert.IsFalse(vendingMachine.IsAStorageIdAlreadyExists(LOCAL_VAR_FAKE_STORAGE_ID));
		}

		// Bastien : For now I will not create more Unit Test because Moq is limited comapred to TypeMock and JustMock
		// And on Mac I can have Microsoft Fakes. Too lazy to install Visual Studio on my Windows.

		//[Description("We control that AddProduct can add product, can't add more than he should")]
		//[Test]
		//public void TestAddProduct()
		//{

		//	// Mocks Configuration
		//	var mock = new Mock<IFactory>();
		//	var mockStorageStub = new Mock<StorageStub>();

		//	mock.Setup(m => m.CreateInstance<StorageStub>()).Returns(It.IsAny<StorageStub>());
		//	mockStorageStub.Setup(m => m.AddOneProduct()).Verifiable();

		//	mockStorageStub.SetupGet(m => m.IsFull).Returns(false);

		//	var vendingMachine = new VendingMachine<StorageStub>(CLASS_VAR_MAX_STORAGE);
		//	vendingMachine.NumberMaxProductsByStorage = 15;

		//	vendingMachine.CreateNewStorage(CLASS_VAR_DEFAULT_STORAGE_ID);
		//	vendingMachine.AddProduct(CLASS_VAR_DEFAULT_STORAGE_ID);

		//	Assert.AreEqual(vendingMachine.NumberOfProductsLeft, 1);

		//}

	}
}

