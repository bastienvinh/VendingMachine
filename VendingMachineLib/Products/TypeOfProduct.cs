using System;
namespace Com.Bvinh.Vendingmachine
{

	// For now, there are two type of products drink and softdrinks. We use drink for or example
	// But it can be Enegetic food, hot drink (yes japanese had this), hot food (yes japanse has it too ><)
	// it can also be smoke products, medecine, toy, gadegets, umbrella, condom, whatever

	// Remark : up to you to add your type of products. Because you will need it to make LINQ queries

	public enum TypeOfProduct
	{
		SOFT_DRINKS = 1,
		GENERAL_FOOD = 2
	}
}

