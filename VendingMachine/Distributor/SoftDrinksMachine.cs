using System;
using System.Linq;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Linq;
using Com.Bvinh.Vendingmachine.Exceptions;
using Functional.Maybe;
using System.Collections;
using System.Collections.Generic;

namespace Com.Bvinh.Test.Vending
{

	// Explaination :
	//
	// This is up to you to define your sceneraio, your products
	// My lib only simulate a vending machine and nothing more
	// Because in reality a vending machine is more complex and different


	/// <summary>
	/// This is just test class, this a not a real class.
	/// This class will help us to define our exercice
	/// </summary>
	public class SoftDrinksMachine
	{
		
		#region Constants
		public const string SODA_SPRITE_RANGE_1 = "C1";
		public const string SODA_SPRITE_RANGE_2 = "C2";

		public const string SODA_COKE_RANGE_1 = "D1";
		public const string SODA_COKE_RANGE_2 = "D2";
		public const string SODA_COKE_RANGE_3 = "D3";

		public const string SODA_FANTA_RANGE_1 = "A1";
		public const string SODA_FANTA_RANGE_2 = "A2";
		public const string SODA_FANTA_RANGE_3 = "A3";

		public const string SODA_JUICE_RANGE_1 = "D1";
		public const string SODA_JUICE_RANGE_2 = "D2";

		private const int MAX_SODA_BY_RANGE = 10;
		#endregion

		#region Attributes
		private VendingMachine<OldFashionStorageVM> _machine;
		#endregion

		#region Constructor
		public SoftDrinksMachine()
		{
			_machine = new VendingMachine<OldFashionStorageVM>(10);
			_machine.NumberMaxProductsByStorage = 10;

			_machine.AddMoneyAuthorizedMoney(Money.P1, Money.P2, Money.P5, Money.P10);
		}
		#endregion


		#region Drinks

		public void AddMoreSprite()
		{
			CheckStorage(SodaCanDrinks.SpriteCanDrinks, SODA_SPRITE_RANGE_1, SODA_SPRITE_RANGE_2);
			AddMoreOneProduct(SODA_SPRITE_RANGE_1, SODA_SPRITE_RANGE_2);
		}

		public void AddMoreCoke()
		{
			CheckStorage(SodaCanDrinks.CokeCanDrink, SODA_COKE_RANGE_1, SODA_COKE_RANGE_2, SODA_COKE_RANGE_3);
			AddMoreOneProduct(SODA_COKE_RANGE_1, SODA_COKE_RANGE_2, SODA_COKE_RANGE_3);
		}

		public void AddMoreFanta()
		{
			CheckStorage(SodaCanDrinks.FantaCanDrink, SODA_FANTA_RANGE_1, SODA_FANTA_RANGE_2, SODA_FANTA_RANGE_3);
			AddMoreOneProduct(SODA_FANTA_RANGE_1, SODA_FANTA_RANGE_2, SODA_FANTA_RANGE_3);
		}

		public void AddMoreJuice()
		{
			CheckStorage(SodaCanDrinks.JuiceCanDrinks, SODA_JUICE_RANGE_1, SODA_JUICE_RANGE_1);
			AddMoreOneProduct(SODA_JUICE_RANGE_1, SODA_JUICE_RANGE_1);
		}


		#endregion


		#region Money


		public void AddMoney(Money money)
		{
			
		}

		#endregion



		#region Storage

		/// <summary>
		/// Add a new product and check any place available and just put one of them in.
		/// </summary>
		/// <returns>The more one product.</returns>
		/// <param name="IdsStorage">Identifiers storage.</param>
		private void AddMoreOneProduct(params string[] IdsStorage)
		{
			var anyStorageId = IdsStorage.FirstMaybe(id => _machine.IsThisStorageIsEmpty(id));
			(anyStorageId.HasValue).IfTrue(() => _machine.AddProduct(anyStorageId.Value));
		}


		/// <summary>
		/// Remove a product from one of storages entries
		/// </summary>
		/// <returns>The one product.</returns>
		/// <param name="IdsStorage">Identifiers storage.</param>
		private void RemoveOneProduct(params string[] IdsStorage)
		{
			var anyStorageId = IdsStorage.FirstMaybe(id => !_machine.IsThisStorageIsEmpty(id));
			(anyStorageId.HasValue).IfFalse(() => _machine.RemoveProduct(anyStorageId.Value));
		}


		/// <summary>
		/// Remove all storages
		/// </summary>
		/// <returns>The storage.</returns>
		/// <param name="idsStorage">Identifiers storage.</param>
		private void CheckStorage(Product p, params string[] idsStorage)
		{
			(idsStorage.Any()).IfFalse( () => { throw new ArgumentOutOfRangeException(nameof(idsStorage), "At least one elements"); } );

			// Yes you can do the two things in the same time, you need to create all the storage first if neccessary before you can check if there still place.
			idsStorage.ForEach((id) => { CheckStorageIfNotExists(id, p); });

			var maybeAnyIdFound = idsStorage.FirstMaybe(id => _machine.StillHavePlaceOnAStorage(id));
			(maybeAnyIdFound.HasValue).IfFalse(() => { throw VMExceptionUtils.StorageIsFull(); });
		}

		/// <summary>
		/// We create a storage if didn't exist
		/// </summary>
		/// <param name="idStorage">Identifier storage.</param>
		/// <param name="pr">Pr.</param>
		private void CheckStorageIfNotExists(string idStorage, Product pr) =>
			_machine.IsAStorageIdAlreadyExists(idStorage)
		          .IfFalse(() => _machine.CreateNewStorage(idStorage, pr.Price));

		// OF course we cheat, this only an example
		// Total = 40 + 20 + 75 + 50 = 185 
		public void FillStartMoney() => _machine.FillMoney(new Dictionary<Money, int> { 
			{ Money.P1, 40 },
			{ Money.P2, 10 },
			{ Money.P5, 15 },
			{ Money.P10, 5 }
		});

		public void ShowOnConsoleTotalOnMachine()
		{
			Console.WriteLine("Total : {0}p", _machine.GetTotalMoneyMachine());
		}
		

		#endregion
	}
}

