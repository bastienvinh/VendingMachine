using System;
namespace Com.Bvinh.Vendingmachine
{
	public interface IFactory
	{
		T CreateInstance<T>(params object[] args) where T : IStorageVMProducts;
	}
}

