using System;
using System.Linq;
using Com.Bvinh.Vendingmachine;
using Com.Bvinh.Linq;
using Com.Bvinh.Vendingmachine.Exceptions;
using Functional.Maybe;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
	public class SoftDrinksMachineSimulation
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

		public const string SODA_JUICE_RANGE_1 = "E1";
		public const string SODA_JUICE_RANGE_2 = "E2";

		private const int MAX_SODA_BY_RANGE = 10;
		#endregion

		#region Attributes
		private VendingMachine<OldFashionStorageVM> _machine;
		#endregion

		#region Constructor
		public SoftDrinksMachineSimulation()
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
			CheckStorage(SodaCanDrinks.JuiceCanDrinks, SODA_JUICE_RANGE_1, SODA_JUICE_RANGE_2);
			AddMoreOneProduct(SODA_JUICE_RANGE_1, SODA_JUICE_RANGE_2);
		}

		public void AddAllDrinks()
		{
			Xfb.Range(30).ForEach((i) => { AddMoreCoke(); });
			Xfb.Range(20).ForEach((i) => { AddMoreSprite(); });
			Xfb.Range(30).ForEach((i) => { AddMoreFanta(); });
			Xfb.Range(20).ForEach((i) => { AddMoreJuice(); });
		}

		#endregion

		#region Money


		public void AddMoney(Money money)
		{
			
		}

		public void CancelAndGetBackMoney()
		{
			var lists = _machine.GetMoneyBackFromVM();

			StringBuilder builder = new StringBuilder("{");
			lists.ForEach((tupleMoney) =>
			{
				builder.AppendFormat(" , {0} : {1}", tupleMoney.Item2.Name, tupleMoney.Item1);
			});
			builder.Append(" }");

			Console.WriteLine(builder.ToString().Replace("{ ,", "{ "));
		}

		public void ClientPutSomeMoney()
		{
			_machine.ClientPutMoney(Money.P10);
			_machine.ClientPutMoney(Money.P2);
		}

		public void ShowHowManyTheClientSpentForNow() => Console.WriteLine("Spent By Client : {0}p", _machine.CurrentMoneyClient);

		#endregion



		#region Storage

		/// <summary>
		/// Add a new product and check any place available and just put one of them in.
		/// </summary>
		/// <returns>The more one product.</returns>
		/// <param name="IdsStorage">Identifiers storage.</param>
		private void AddMoreOneProduct(params string[] IdsStorage)
		{
			var anyStorageId = IdsStorage.FirstMaybe(id => _machine.StillHavePlaceOnAStorage(id));
			(anyStorageId.HasValue).IfTrue(() => _machine.AddProduct(anyStorageId.Value));
		}


		/// <summary>
		/// Remove a product from one of storages entries
		/// </summary>
		/// <returns>The one product.</returns>
		/// <param name="IdsStorage">Identifiers storage.</param>
		private void RemoveOneProduct(params string[] IdsStorage)
		{
			var anyStorageId = IdsStorage.FirstMaybe(id => !_machine.StillHavePlaceOnAStorage(id));
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

			// var maybeAnyIdFound = idsStorage.FirstMaybe(id => _machine.StillHavePlaceOnAStorage(id));
			var maybeAnyIdFound = idsStorage.FirstOrDefault(id => _machine.StillHavePlaceOnAStorage(id)).ToMaybe();
			(maybeAnyIdFound.HasValue).IfFalseThrow (VMExceptionUtils.StorageIsFull);
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

		public void ShowOnConsoleTotalOnMachine() => Console.WriteLine("Total : {0}p", _machine.GetTotalMoneyMachine());

		/// <summary>
		/// Shows the on console the diffrent product.
		/// Remark : In real-life, you will have sometimes a mix a product. It's depend how the supplier add his product. The product really nothing and on the storage count.
		/// It's impossible to predict if he will put this products. It can be like that : https://www.youtube.com/watch?v=dzPuPWJq3I8 or like that : https://www.youtube.com/watch?v=Ts4WcBHbukY
		/// And the second choice can be really tricky, because they can put anything they want. I saw sometimes, with another product in the middle.
		/// So don't car about the product, but about the storage.
		/// </summary>
		public void ShowOnConsoleTheDiffrentProduct()
		{
			Console.WriteLine("Sprite => {0}" , SODA_SPRITE_RANGE_1);
			Console.WriteLine("Sprite => {0}", SODA_SPRITE_RANGE_2);
			Console.WriteLine("Coke => {0}", SODA_COKE_RANGE_1);
			Console.WriteLine("Coke => {0}", SODA_COKE_RANGE_2);
			Console.WriteLine("Coke => {0}", SODA_COKE_RANGE_3);
			Console.WriteLine("Fanta => {0}", SODA_FANTA_RANGE_1);
			Console.WriteLine("Fanta => {0}", SODA_FANTA_RANGE_2);
			Console.WriteLine("Fanta => {0}", SODA_FANTA_RANGE_3);
			Console.WriteLine("Juice => {0}", SODA_JUICE_RANGE_1);
			Console.WriteLine("Juice => {0}",  SODA_JUICE_RANGE_2);
		}

		public void ShowFullStorage()
		{
			_machine.StatesStorage()
							.ForEach((id, units) => Console.WriteLine("Storage : {0}  Units : {1}", id, units));
		}

		#endregion


		#region Client Orders

		/// <summary>
		/// A client always on this kind of Vending Machine
		/// Show on console if the person could take a drink or not
		/// </summary>
		/// <param name="idStorage">Identifier storage.</param>
		public void TryGetProduct(string idStorage)
		{
			try
			{
				(_machine.GetProduct(idStorage) == VMErrorCode.CAN_HAVE_PRODUCT)
					.IfTrue(() => Console.WriteLine("Buy a product fron storage {0}", idStorage))
					.IfFalse(() => Console.WriteLine("Error while try to buy in storage {0}", idStorage));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			
		}

		public void BuyAllSprite()
		{
			Xfb.Range(10).ForEach(x =>
			{
				TryGetProduct(SODA_SPRITE_RANGE_1);
				TryGetProduct(SODA_SPRITE_RANGE_2);
			});

		}

		#endregion
	}
}

