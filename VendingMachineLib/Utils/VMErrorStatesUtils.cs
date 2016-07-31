using System;
namespace Com.Bvinh.Vendingmachine.Utils
{


	// This a definition of all errors you can encounter as a Vendor Machine
	// STOPPED                        : A vending maching can sometimes fail or be disabled by the vendor
	// NO_ENOUGH_MONEY                : A vending must have money no matter what to give back money,
	//																	We use it in case where the client has money and there not enough money to give back


	// NO_MORE_DRINKS                 : There are no drinks at all in the machine
	// RESERVE_NOT_AVAILABLE					: When a reseve doesn't have enough drinks
	// NOT_ENOUGH_MONEY_FROM_CUSTOMER : The client doesn't have enough money to pay the vending machine
	// CANCELED_BY_CUSTOMER						: The client has canceled a product

	public enum VMErrorCode
	{
		NONE = 0,
		STOPPED = 1,
		NO_ENOUGH_MONEY = 2,
		NO_MORE_PRODUCTS = 3,
		RESERVE_NOT_AVAILABLE = 4,
		NOT_ENOUGH_MONEY_FROM_CUSTOMER = 5,
		CANCELED_BY_CUSTOMER = 6,
		UNKNOWN_ERROR = 777
	}

	// We will use this class on the purpose to help more easily to retrieve the good errors

	public static class VMErrorStatesUtils
	{
		
	}
}

