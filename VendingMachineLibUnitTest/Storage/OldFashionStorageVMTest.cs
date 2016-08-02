using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Vendingmachine.Utils;
using Moq;
using Moq.Protected;
using Moq.Language;

namespace VendingMachineLibUnitTest
{
	[TestFixture]
	public class OldFashionStorageVMTest
	{
		private const int CLASS_VAR_A_GOOD_CAPACITY_NUMBER = 10;

		[Description("All storage has instance creator from our generic factory.")]
		[Test]
		public void TestHaveAFactoryInstanceOfOurStorage()
		{
			var aStorage = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			Assert.IsNotNull(aStorage);
			Assert.IsInstanceOf<OldFashionStorageVM>(aStorage);
		}

		[Description("OldFashionStorageVM can't set max capacity to 0 or lower")]
		[Test]
		public void IsOldFashionStorageVMCanSetToEmptyOrBelow()
		{
			const string LOCAL_CONST_ERROR_MESSAGE = "Your storage must have a capacity > 0.";
			Assert.Throws<StorageException>(() => { StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(0); }, LOCAL_CONST_ERROR_MESSAGE);
			Assert.Throws<StorageException>(() => { StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(-3); }, LOCAL_CONST_ERROR_MESSAGE);

			// Test -3
			Assert.Throws<StorageException>(() =>
			{
				(StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(3)).SetCapacity(-3);
			}, LOCAL_CONST_ERROR_MESSAGE);

			// Test 0
			Assert.Throws<StorageException>(() =>
			{
				var testNewInstance = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(3);
				testNewInstance.SetCapacity(0);
			}, LOCAL_CONST_ERROR_MESSAGE);
		}


		[Description("Test price is correct. The price can't be negative, the price must be zero or more. " +
		             "The price must me the same as specified.")]
		[Test]
		public void TestPriceIsCorrect()
		{
			const double LOCAL_CONST_BAD_PRICE_TEST = -99;
			const double LOCAL_CONST_GOOD_PRICE_TEST = 10;
			const double LOCAL_CONST_FREE_PRICE_TEST = 0;
			const string MESSAGE_ERROR_ABOUT_CORRUPTED_PRICE = "The price can't be negative";

			//var aStorageForVM = new OldFashionStorageVM(10);
			var aStorageForVM = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(10);

			Assert.AreEqual(aStorageForVM.Price, LOCAL_CONST_FREE_PRICE_TEST); // Always free or equal to zero at start


			// Test bad price
			Assert.Throws<StorageException>(() => { aStorageForVM.Price = LOCAL_CONST_BAD_PRICE_TEST; }, 
			                                MESSAGE_ERROR_ABOUT_CORRUPTED_PRICE);


			// Test the good price
			Assert.DoesNotThrow(() => { aStorageForVM.Price = LOCAL_CONST_GOOD_PRICE_TEST; });
			Assert.AreEqual(aStorageForVM.Price, LOCAL_CONST_GOOD_PRICE_TEST);


			// Test he 0 case
			Assert.DoesNotThrow(() => { aStorageForVM.Price = LOCAL_CONST_FREE_PRICE_TEST; });
			Assert.AreEqual(aStorageForVM.Price, LOCAL_CONST_FREE_PRICE_TEST);
		}


		[Description("The Identifier of storage is correct at start with any constuctor and can't be changed.")]
		[Test]
		public void TestTheIDStorageIsCorrect()
		{
			// TODO : Do some reflection programming to prove that the class can't set IdStorage
			const string LOCAL_ERROR_MESSAGE_ON_BAD_ID = "IdStorage can't be null, empty or with whitespace.";
			const string LOCAL_CONST_BAD_ID_STORAGE_TEST_EMPTY = "";
			const string LOCAL_CONST_BAD__ID_STORAGE_TEST_WHITESPACE = "       ";
			const string LOCAL_CONST_GOOD_ID_STORAGE = "GoodMornding";

			// var aStorageFromVM = new OldFashionStorageVM(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			var aStorageFromVM = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			Assert.IsNotNull(aStorageFromVM.IdStorage);
			Assert.IsNotEmpty(aStorageFromVM.IdStorage);

			Assert.DoesNotThrow(() =>
			{
				aStorageFromVM = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>( LOCAL_CONST_GOOD_ID_STORAGE, 
				                                                                             CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
				//aStorageFromVM = new OldFashionStorageVM(LOCAL_CONST_GOOD_ID_STORAGE, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			});

			Assert.AreEqual(aStorageFromVM.IdStorage, LOCAL_CONST_GOOD_ID_STORAGE);

			// Test Empty Case
			Assert.Throws<StorageException>(() =>
			{
				StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(LOCAL_CONST_BAD_ID_STORAGE_TEST_EMPTY, 
				                                                            CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
				//new OldFashionStorageVM(LOCAL_CONST_BAD_ID_STORAGE_TEST_EMPTY, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			}, LOCAL_ERROR_MESSAGE_ON_BAD_ID);


			// Test Whitespace Case
			Assert.Throws<StorageException>(() =>
			{
				StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(LOCAL_CONST_BAD__ID_STORAGE_TEST_WHITESPACE, 
				                                                            CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
				// new OldFashionStorageVM(LOCAL_CONST_BAD__ID_STORAGE_TEST_WHITESPACE, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			}, LOCAL_ERROR_MESSAGE_ON_BAD_ID);

		}

		[Description("We verify that the current capcity is correct. " +
		             "So we simulate that that we can add and see the amount is correct.")]
		[Test]
		public void TestTheCurrentCapacityIsCorrect()
		{
			// TODO : In this case we should use a mock because for the test CurrentCapcity is dependant of add / clear / remove

			// Test when we add

			//var aStorageVM = new OldFashionStorageVM(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			var aStorageVM = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);

			Assert.IsTrue(aStorageVM.CurrentCapacity >= 0);

			aStorageVM.AddOneProduct();
			aStorageVM.AddOneProduct();
			aStorageVM.AddOneProduct();
			aStorageVM.AddOneProduct();

			Assert.AreEqual(aStorageVM.CurrentCapacity, (CLASS_VAR_A_GOOD_CAPACITY_NUMBER - 4));

			// Test when we remove

			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();

			Assert.AreEqual(aStorageVM.CurrentCapacity, (CLASS_VAR_A_GOOD_CAPACITY_NUMBER - 2));

			// Test when storage is empty

			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();
			aStorageVM.RemoveOneProduct();

			Assert.AreEqual(aStorageVM.CurrentCapacity, aStorageVM.MaxCapacity);
		}


		[Description("When we add a product we can't go further than the maxhimun capacity. A storage is always limited." +
		             "This is not Futurama.")]
								
		[Test]
		public void TestAddCantGoFurtherThanMaxCapacity()
		{
			const int LOCAL_CONST_LIMIT_TEST = 10;
			const string LOCAL_CONST_ERROR_MESSAGE_OVERCAPACITY_OF_STORAGE = "You can't add more on this storage.";

			//var aStorageVM = new OldFashionStorageVM(LOCAL_CONST_LIMIT_TEST);
			var aStorageVM = StorageFactory.Instance.CreateInstance<OldFashionStorageVM>(LOCAL_CONST_LIMIT_TEST);

			Assert.Throws<StorageException>(() =>
			{
				// We add 12 times a product to the storage to add more products than the capacity
				Enumerable.Range(0, LOCAL_CONST_LIMIT_TEST + 2).ForEach((x) => aStorageVM.AddOneProduct());

			}, LOCAL_CONST_ERROR_MESSAGE_OVERCAPACITY_OF_STORAGE);

		}



	}
}

