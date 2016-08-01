using System;
using NUnit.Framework;
using NUnit;
using Com.Bvinh.Vendingmachine;

namespace VendingMachineLibUnitTest
{
	[TestFixture]
	public class OldFashionStorageVMTest
	{

		private const int CLASS_VAR_A_GOOD_CAPACITY_NUMBER = 10;

		[Description("OldFashionStorageVM can't set max capacity to 0 or lower")]
		[Test]
		public void IsOldFashionStorageVMCanSetToEmptyOrBelow()
		{
			string errorMessage = "Your storage must have a capacity > 0.";
			Assert.Throws<StorageException>(() => { new OldFashionStorageVM(0); }, errorMessage);
			Assert.Throws<StorageException>(() => { new OldFashionStorageVM(-3); }, errorMessage);

			// Test -3
			Assert.Throws<StorageException>(() =>
			{
				(new OldFashionStorageVM(3)).SetCapacity(-3);
			}, errorMessage);

			// Test 0
			Assert.Throws<StorageException>(() =>
			{
				(new OldFashionStorageVM(3)).SetCapacity(0);
			}, errorMessage);
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

			OldFashionStorageVM aStorageForVM = new OldFashionStorageVM(10);

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

			OldFashionStorageVM aStorageFromVM = new OldFashionStorageVM(CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			Assert.IsNotNull(aStorageFromVM.IdStorage);
			Assert.IsNotEmpty(aStorageFromVM.IdStorage);

			Assert.DoesNotThrow(() =>
			{
				aStorageFromVM = new OldFashionStorageVM(LOCAL_CONST_GOOD_ID_STORAGE, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			});

			Assert.AreEqual(aStorageFromVM.IdStorage, LOCAL_CONST_GOOD_ID_STORAGE);

			// Test Empty Case
			Assert.Throws<StorageException>(() =>
			{
				new OldFashionStorageVM(LOCAL_CONST_BAD_ID_STORAGE_TEST_EMPTY, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			}, LOCAL_ERROR_MESSAGE_ON_BAD_ID);


			// Test Whitespace Case
			Assert.Throws<StorageException>(() =>
			{
				new OldFashionStorageVM(LOCAL_CONST_BAD__ID_STORAGE_TEST_WHITESPACE, CLASS_VAR_A_GOOD_CAPACITY_NUMBER);
			}, LOCAL_ERROR_MESSAGE_ON_BAD_ID);


		}

	}
}

