using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit;
using Com.Bvinh.Vendingmachine.Exceptions;
using Com.Bvinh.Linq;
using Com.Bvinh.Vendingmachine;
using Functional.Maybe;
using FluentAssertions.Collections;
using FluentAssertions;



namespace VendingMachineLibUnitTest
{
	[Description("This a scenerio of Vending Machine with OldFashiondStorage.")]
	[TestFixture]
	public class VendingMachineWithOldFashionedStorageTest
	{

		#region Constants

		const int NUMBER_RESERVE = 10;
		const int MAX_CAPACITY_PER_STORAGE = 10;


		// On a classic vending machine we have a combinaison of a letter and a number
		const string COKE_ID_STORAGE_1 = "C1";
		const string COKE_ID_STORAGE_2 = "C2";

		#endregion


		#region Attributes
		private VendingMachine<OldFashionStorageVM> _storageToTest;
		#endregion


		[OneTimeSetUp]
		public void Init()
		{
			_storageToTest = new VendingMachine<OldFashionStorageVM>(NUMBER_RESERVE);
			_storageToTest.NumberMaxProductsByStorage = MAX_CAPACITY_PER_STORAGE;
		}


		[Test, Order(1)]
		public void TestTheSetup()
		{
			Assert.IsNotNull(_storageToTest);
			Assert.AreEqual(MAX_CAPACITY_PER_STORAGE, _storageToTest.NumberMaxProductsByStorage);
			Assert.AreEqual(NUMBER_RESERVE, _storageToTest.NummberOfReserve);

			// Check the errors that should happens
			// TODO : do that
		}

		[Description("Add two new storage for coke")]
		[Test, Order(2)]
		public void TestAddStorage()
		{
			_storageToTest.CreateNewStorage(COKE_ID_STORAGE_1, SodaCanDrinks.CokeCanDrink.Price);
			_storageToTest.CreateNewStorage(COKE_ID_STORAGE_2, SodaCanDrinks.CokeCanDrink.Price);

			// Get neccessary informations
			var listsToVerify = _storageToTest.GetStoragesIds();


			// Test
			listsToVerify
				.Should().HaveCount(2, "Because we put two idStorage")
				.And.Contain(new[] { COKE_ID_STORAGE_1, COKE_ID_STORAGE_2 });

			_storageToTest.IsAStorageIdAlreadyExists(COKE_ID_STORAGE_1).Should().BeTrue();
			_storageToTest.IsAStorageIdAlreadyExists(COKE_ID_STORAGE_1).Should().BeTrue();
			// TODO : Add test capacity
		}

		[Description("We add coke to the storage and see what happen.")]
		[Test, Order(3)]
		public void TestAddToStorageCokeStorage()
		{
			// We add 5 cokes
			Xfb.Range(5).ForEach((i) => _storageToTest.AddProduct(COKE_ID_STORAGE_1));

			_storageToTest.StatesStorage()
										.Should().ContainKey(COKE_ID_STORAGE_1)
										.And.ContainKey(COKE_ID_STORAGE_2)
										.And.Contain(new KeyValuePair<string, int>(COKE_ID_STORAGE_1, 5))
										.And.Contain(new KeyValuePair<string, int>(COKE_ID_STORAGE_2, 0));

			_storageToTest.StillHavePlaceOnAStorage(COKE_ID_STORAGE_1).Should().BeTrue();
			_storageToTest.StillHavePlaceOnAStorage(COKE_ID_STORAGE_2).Should().BeTrue();
		}


		[Description("We fill entirely the storage ti ensure we have a limit on our storage")]
		[Test, Order(4)]
		public void TestToFillAllTheStorage()
		{
			// We already fill 5 place on the first storage, we fill the rest
			// So wee fill 5 on the first storage and 10 and the second storage
			Xfb.Range(5).ForEach((i) => _storageToTest.AddProduct(COKE_ID_STORAGE_1));
			Xfb.Range(10).ForEach((i) => _storageToTest.AddProduct(COKE_ID_STORAGE_2));

			_storageToTest.StatesStorage()
										.Should().ContainKeys(new string[] { COKE_ID_STORAGE_1, COKE_ID_STORAGE_2 }, "Because we have 2 elements")
										.And.Contain(new KeyValuePair<string, int>(COKE_ID_STORAGE_1, 10), "The first storage should have 10 cans of coke.")
										.And.Contain(new KeyValuePair<string, int>(COKE_ID_STORAGE_2, 10), "The second storage should have 10 cans of coke too.");
		}

		[Description("We can't add more products on the storage")]
		[Test, Order(5)]
		public void TestCanAddMoreStorageOnIt()
		{
		}

	}
}

