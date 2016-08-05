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
	/// <summary>
	/// Scenario : Simualation ,that means they don't need to test very thing. This isn't a Unit Test
	/// Scenario about our Vending Machine with a OldFashionnedStorage
	/// All test are in Order
	/// </summary>
	[Description("This is a scenerio of Vending Machine with OldFashiondStorage.")]
	[TestFixture]
	public class VendingMachineWithOldFashionedStorageTest
	{

		#region Constants

		const int NUMBER_RESERVE = 12;
		const int MAX_CAPACITY_PER_STORAGE = 10;

		// On a classic vending machine we have a combinaison of a letter and a number
		const string COKE_ID_STORAGE_1 = "C1";
		const string COKE_ID_STORAGE_2 = "C2";
		const string COKE_ID_STORAGE_3 = "C3";
		const string COKE_ID_STORAGE_4 = "C4";

		const string JUICE_ID_STORAGE_1 = "A1";
		const string JUICE_ID_STORAGE_2 = "A2";
		const string JUICE_ID_STORAGE_3 = "A3";

		const string SPRITE_ID_STORAGE_1 = "D1";
		const string SPRITE_ID_STORAGE_2 = "D2";

		const string FANTA_ID_STORAGE_1 = "B1";
		const string FANTA_ID_STORAGE_2 = "B2";
		const string FANTA_ID_STORAGE_3 = "B3";

		#endregion


		#region Attributes
		private VendingMachine<OldFashionStorageVM> _vendingMachineToTest;
		private List<string> _listsIdsStorage;
		#endregion


		[OneTimeSetUp]
		public void Init()
		{
			_listsIdsStorage = new List<string> { COKE_ID_STORAGE_1, COKE_ID_STORAGE_2, COKE_ID_STORAGE_3, COKE_ID_STORAGE_4,
				FANTA_ID_STORAGE_1, FANTA_ID_STORAGE_2, FANTA_ID_STORAGE_3,
				JUICE_ID_STORAGE_1, JUICE_ID_STORAGE_2, JUICE_ID_STORAGE_3,
				SPRITE_ID_STORAGE_1, SPRITE_ID_STORAGE_2
			};

			_vendingMachineToTest = new VendingMachine<OldFashionStorageVM>(_listsIdsStorage.Count());
			_vendingMachineToTest.NumberMaxProductsByStorage = MAX_CAPACITY_PER_STORAGE;
			_vendingMachineToTest.AddMoneyAuthorizedMoney(new[] { Money.P1, Money.P2, Money.P5, Money.P10 });

			// We need to fill some changes in case the first customer have more than a drink
			_vendingMachineToTest.FillMoney(new Dictionary<Money, int> {
				{ Money.P1, 40 },
				{ Money.P2, 10 },
				{ Money.P5, 15 },
				{ Money.P10, 5 }
			});

		}

		[Test, Order(1)]
		public void TestTheSetup()
		{

			// We check all default configurations
			_vendingMachineToTest.Should().NotBeNull();
			_vendingMachineToTest.NumberMaxProductsByStorage.Should().Be(MAX_CAPACITY_PER_STORAGE, "The max storage was set to 10.");
			_vendingMachineToTest.NummberOfReserve.Should().Be(NUMBER_RESERVE, "The numbr of reserve shoud be 10");

			// We chekck all the moneys that we can only put in the machine
			_vendingMachineToTest.GetListAuthorizedMoney()
			              .Should().HaveCount(4, "You must 4 moneys auhtorized")
			              .And.Contain(new[] { Money.P1, Money.P2, Money.P5, Money.P10 }, "Only p1, p2, p5 and p10 are authorized");

			// we check the moneys we put for the changes
			// 40 + 20 + 75 + 50
			_vendingMachineToTest.GetTotalMoneyMachine().Should().Be(185, "Because we add P1 : 40, P2 : 10, P3 : 15, P4 : 5");
		}

		[Description("Add two new storage for coke")]
		[Test, Order(2)]
		public void TestAddStorageAllProduct()
		{
			// verify that our storage  is empty
			_vendingMachineToTest.HasEmptyStorage.Should().BeTrue();


			// We create our storage with a price
			_vendingMachineToTest.CreateNewStorage(COKE_ID_STORAGE_1, SodaCanDrinks.CokeCanDrink.Price);
			_vendingMachineToTest.CreateNewStorage(COKE_ID_STORAGE_2, SodaCanDrinks.CokeCanDrink.Price);
			_vendingMachineToTest.CreateNewStorage(COKE_ID_STORAGE_3, SodaCanDrinks.CokeCanDrink.Price);
			_vendingMachineToTest.CreateNewStorage(COKE_ID_STORAGE_4, SodaCanDrinks.CokeCanDrink.Price);

			_vendingMachineToTest.CreateNewStorage(FANTA_ID_STORAGE_1, SodaCanDrinks.FantaCanDrink.Price);
			_vendingMachineToTest.CreateNewStorage(FANTA_ID_STORAGE_2, SodaCanDrinks.FantaCanDrink.Price);
			_vendingMachineToTest.CreateNewStorage(FANTA_ID_STORAGE_3, SodaCanDrinks.FantaCanDrink.Price);

			_vendingMachineToTest.CreateNewStorage(JUICE_ID_STORAGE_1, SodaCanDrinks.JuiceCanDrinks.Price);
			_vendingMachineToTest.CreateNewStorage(JUICE_ID_STORAGE_2, SodaCanDrinks.JuiceCanDrinks.Price);
			_vendingMachineToTest.CreateNewStorage(JUICE_ID_STORAGE_3, SodaCanDrinks.JuiceCanDrinks.Price);

			_vendingMachineToTest.CreateNewStorage(SPRITE_ID_STORAGE_1, SodaCanDrinks.SpriteCanDrinks.Price);
			_vendingMachineToTest.CreateNewStorage(SPRITE_ID_STORAGE_2, SodaCanDrinks.SpriteCanDrinks.Price);

			// Get neccessary informations
			var listsToVerify = _vendingMachineToTest.GetStoragesIds();


			// Test
			listsToVerify
				.Should().HaveCount(12, "Because we put two idStorage")
				.And.Contain(new[] { COKE_ID_STORAGE_1, COKE_ID_STORAGE_2, 
				COKE_ID_STORAGE_2, COKE_ID_STORAGE_3, COKE_ID_STORAGE_4, FANTA_ID_STORAGE_1, FANTA_ID_STORAGE_2, FANTA_ID_STORAGE_3,
				JUICE_ID_STORAGE_1, JUICE_ID_STORAGE_2, JUICE_ID_STORAGE_3, SPRITE_ID_STORAGE_1, SPRITE_ID_STORAGE_2 });

			// Verify Coke Cans Storages
			_vendingMachineToTest.IsAStorageIdAlreadyExists(COKE_ID_STORAGE_1).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(COKE_ID_STORAGE_2).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(COKE_ID_STORAGE_3).Should().BeTrue();

			// Verify Fanta Cans Storages
			_vendingMachineToTest.IsAStorageIdAlreadyExists(FANTA_ID_STORAGE_1).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(FANTA_ID_STORAGE_2).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(FANTA_ID_STORAGE_3).Should().BeTrue();


			// Verify Juice Cans Storages
			_vendingMachineToTest.IsAStorageIdAlreadyExists(JUICE_ID_STORAGE_1).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(JUICE_ID_STORAGE_2).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(JUICE_ID_STORAGE_3).Should().BeTrue();


			// Verify Sprites Cans Storages
			_vendingMachineToTest.IsAStorageIdAlreadyExists(SPRITE_ID_STORAGE_1).Should().BeTrue();
			_vendingMachineToTest.IsAStorageIdAlreadyExists(SPRITE_ID_STORAGE_2).Should().BeTrue();

			// Try to add one more storage
			Action addNewStorageScope = () => _vendingMachineToTest.CreateNewStorage("FAKE ID", 0);
			addNewStorageScope.ShouldThrow<VMSupplierProductTypeException>("Because you can't add more storage the the maximum");


			// No more storage
			_vendingMachineToTest.HasEmptyStorage.Should().BeFalse();
		}

		[Description("We add coke to the storage and see what happen.")]
		[Test, Order(3)]
		public void TestAddToStorageCokeStorage()
		{

			// We add 5 cokes
			Xfb.Range(5).ForEach((i) => _vendingMachineToTest.AddProduct(COKE_ID_STORAGE_1));

			_vendingMachineToTest.StatesStorage()
										.Should().ContainKey(COKE_ID_STORAGE_1)
										.And.ContainKey(COKE_ID_STORAGE_2)
										.And.Contain(new Dictionary<string, int>
										{
											{ COKE_ID_STORAGE_1, 5 }, { COKE_ID_STORAGE_2, 0 }, { COKE_ID_STORAGE_3, 0 }, { COKE_ID_STORAGE_4, 0 },
											{ FANTA_ID_STORAGE_1, 0 }, { FANTA_ID_STORAGE_2, 0 }, { FANTA_ID_STORAGE_3, 0 },
											{ JUICE_ID_STORAGE_1, 0 }, { JUICE_ID_STORAGE_2, 0 }, { JUICE_ID_STORAGE_3, 0 },
											{ SPRITE_ID_STORAGE_1, 0 }, { SPRITE_ID_STORAGE_2, 0 }
										});


			_listsIdsStorage.TrueForAll(id => _vendingMachineToTest.StillHavePlaceOnAStorage(id)).Should().BeTrue();
		}


		[Description("We fill entirely the storage to ensure we have a limit on our storage")]
		[Test, Order(4)]
		public void TestToFillAllTheStorage()
		{
			// We already fill 5 place on the first storage, we fill the rest
			// So wee fill 5 on the first storage and 10 and the second storage
			Xfb.Range(5).ForEach((i) => _vendingMachineToTest.AddProduct(COKE_ID_STORAGE_1));

			// We fill the other storage
			var listsToFill = _listsIdsStorage.Where(s => s != COKE_ID_STORAGE_1);
			Xfb.Range(10).ForEach(i => listsToFill.ForEach(id => _vendingMachineToTest.AddProduct(id)));

			_vendingMachineToTest.NumberOfProductsLeft.Should().Be(120, "We comsume nothing yet, so we must have 120 left products");
			_listsIdsStorage.TrueForAll(id => !_vendingMachineToTest.StillHavePlaceOnAStorage(id)).Should().BeTrue();
			_vendingMachineToTest.StatesStorage()
										.Should().ContainKeys(_listsIdsStorage)
										.And.ContainValues(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }); // Bastien : Not Sure it works like that


			Action addAnotherProuctionThatShouldntAction = () => { };
			_listsIdsStorage.ForEach(id =>
			{
				addAnotherProuctionThatShouldntAction = () => _vendingMachineToTest.AddProduct(id);
				addAnotherProuctionThatShouldntAction
					.ShouldThrow<VMSupplierProductTypeException>(string.Format("Because you can add more product on this sotrage {0}", id));
			});
			 
		}


		[Description("Test of a client that put money on ")]
		[Test, Order(5)]
		public void TestClientTryToPutMoney()
		{
			// we attempt to add a non-autorized money
			Action actionTryNonAuthorizedMoney = () => _vendingMachineToTest.ClientPutMoney(Money.P20);
			actionTryNonAuthorizedMoney.ShouldThrow<VMClientTypeException>("Because you can't add P20 when you have only P1, P2, P5, P10");


			// We put 15P on the machine
			_vendingMachineToTest.ClientPutMoney(Money.P10);
			_vendingMachineToTest.ClientPutMoney(Money.P5);


			_vendingMachineToTest.CurrentMoneyClient.Should().Be(15);
			_vendingMachineToTest.StillMoneyLeft.Should().BeTrue();
			_vendingMachineToTest.GetTotalMoneyMachine().Should().Be(200); // 185 (machine) + 15 (client)

			// TODO : Make a methods to retrieve the amount of each money
			//	_vendingMachineToTest.GetMoneyBackFromVM().Should()
			//											 .Contain(new Tuple<int, Money>(6, Money.P10))
			//											 .And.Contain(new Tuple<int, Money>(16, Money.P5))
			//											 .And.Contain(new Tuple<int, Money>(40, Money.P1))
			//											 .And.Contain(new Tuple<int, Money>(10, Money.P2));
		}


		[Description("Test that the client can get back his money")]
		[Test, Order(6)]
		public void TestGiveBackMoney()
		{
			_vendingMachineToTest.GetMoneyBackFromVM()
													 .Should().Contain(new Tuple<int, Money>(1, Money.P10))
													 .And.Contain(new Tuple<int, Money>(1, Money.P5));

			_vendingMachineToTest.CurrentMoneyClient.Should().Be(0);
			_vendingMachineToTest.GetTotalMoneyMachine().Should().Be(185);
		}

		[Test]
		public void TestCommandeAllSprite()
		{
			
		}
	}
}

