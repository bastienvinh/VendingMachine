using System;
namespace Com.Bvinh.Vendingmachine.Exceptions
{


	// Error Descriptions
	// This a definition of all errors you can encounter as a Vendor Machine
	// 
	// STOPPED                        				: A vending maching can sometimes fail or be disabled by the vendor
	// NO_ENOUGH_MONEY                				: A vending must have money no matter what to give back money,
	//																					We use it in case where the client has money and there not enough money to give back
	//
	// NO_MORE_PRODUCTS                 			: There are no drinks at all in the machine
	// RESERVE_NOT_AVAILABLE									: When a reseve doesn't have enough drinks
	// NOT_ENOUGH_MONEY_FROM_CUSTOMER 				: The client doesn't have enough money to pay the vending machine
	// CANCELED_BY_CUSTOMER										: The client has canceled a product
	// UNKNOWN_ERROR													: Generic we can't destribe
	//
	// MAX_MONEY_CANT_BE_NEGATIVE							: Money is always positive
	// MAX_MONEY_IS_INFERIOR_TO_CURRENT_MONEY : When you set money to the machine and it's under the current money, 
	// 																					you have more chance to provoke error. 
	// 																					So we raise a error to ensure that case should never happen ine the first place

	/// <summary>
	/// Code Error that you could find during your Vending Machine Operations
	/// </summary>
	public enum VMErrorCode
	{
		NONE = 0,
		STOPPED = 1,
		NO_ENOUGH_MONEY = 2,
		NO_MORE_PRODUCTS = 3,
		RESERVE_NOT_AVAILABLE = 4,
		NOT_ENOUGH_MONEY_FROM_CUSTOMER = 5,
		CANCELED_BY_CUSTOMER = 6,
		NO_MORE_STORAGE_AVAILABLE = 7,
		MAX_MONEY_CANT_BE_NEGATIVE = 8,
		MAX_MONEY_IS_INFERIOR_TO_CURRENT_MONEY = 9,
		STORAGE_MAX_CAPACITY_ILLEGAL_NUMBER = 10, 
		STORAGE_ALREADY_EXISTS = 11,
		STORAGE_DOESNT_EXISTS = 12,
		STORAGE_IS_FULL = 13,
		UNKNOWN_ERROR = 777
	}

	// We will use this class on the purpose to help more easily to retrieve the good errors


	public static class VMExceptionUtils
	{

		// Bastien : I shall create a enumration pattern than normal enumeration, not sure (erase this comment at your ease)

		/// <summary>
		/// Will create new Exception type VMSupplierProductTypeException because the Vending Machine is full 
		/// and had no more place for another storage
		/// </summary>
		/// <returns>New VMSupplierProductTypeException to use when there are no more reserve</returns>
		public static VMSupplierProductTypeException NoMoreReserve()
		{
			return new VMSupplierProductTypeException(VMErrorCode.NO_MORE_PRODUCTS, 
			                                          "No more storage available in the current Vending Machine");
		}

		/// <summary>
		/// Max Money can't be negative, because is always positive
		/// </summary>
		/// <returns>The money cant be negative.</returns>
		public static VMSupplierProductTypeException MaxMoneyCantBeNegative() => 
		new VMSupplierProductTypeException(VMErrorCode.MAX_MONEY_CANT_BE_NEGATIVE, 
		                                   "Max Money can't be negative");


		/// <summary>
		/// Exception to prevent malfunctionning when your max of money id superior to current money stored. 
		/// </summary>
		/// <returns>The money cant be inf to current money.</returns>
		public static VMSupplierProductTypeException MaxMoneyCantBeInfToCurrentMoney() =>
		new VMSupplierProductTypeException(VMErrorCode.MAX_MONEY_IS_INFERIOR_TO_CURRENT_MONEY,
																			 "You can't set a the max when you have more money than that." +
		                                   "So clear the Vending Machine or Set something superior to the current money stores");



		/// <summary>
		/// Exception when there no more products on the machines.
		/// </summary>
		/// <returns>The no product on the vending machine.</returns>
		public static VMSupplierProductTypeException HasNoProductOnTheVendingMachine() =>
		new VMSupplierProductTypeException(VMErrorCode.NO_MORE_PRODUCTS, "There no more products. The supplier must fill it.");


		/// <summary>
		/// Exception to manage the maximum of a storage. Because it must be superior to 0.
		/// </summary>
		/// <returns>The have zero or lower as storage max capacity.</returns>
		public static VMSupplierProductTypeException CanHaveZeroOrLowerAsStorageMaxCapacity() =>
		new VMSupplierProductTypeException(VMErrorCode.STORAGE_MAX_CAPACITY_ILLEGAL_NUMBER, 
		                                   "The capacity max of a Storage must me superior to zero. Or set the capacity.");


		/// <summary>
		/// Exception to signal if Container exists
		/// </summary>
		/// <returns>The already exists.</returns>
		public static VMSupplierProductTypeException StorageAlreadyExists() =>
		new VMSupplierProductTypeException(VMErrorCode.STORAGE_ALREADY_EXISTS, "The Storage already exists.");


		/// <summary>
		/// Exception when a supplier try to use a non-existed storage.
		/// </summary>
		/// <returns>The doesnt exists.</returns>
		public static VMSupplierProductTypeException StorageDoesntExists() =>
		new VMSupplierProductTypeException(VMErrorCode.STORAGE_DOESNT_EXISTS, "This storage doesn't exist");


		/// <summary>
		/// Exception when your storage is completly full
		/// </summary>
		/// <returns>The is full.</returns>
		public static VMSupplierProductTypeException StorageIsFull() =>
		new VMSupplierProductTypeException(VMErrorCode.STORAGE_IS_FULL, "The storage is full and can't add more product");
	}
}

