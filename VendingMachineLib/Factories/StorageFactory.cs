using System;
using System.Reflection;

namespace Com.Bvinh.Vendingmachine
{
	/// <summary>
	/// This factory will help us to create a new instance of storage
	/// </summary>
	public sealed class StorageFactory : IFactory
	{

		#region Attributes
		private static StorageFactory SelfInstance;
		#endregion

		#region Properties

		public static StorageFactory Instance
		{
			get 
			{
				if (SelfInstance == null)
					SelfInstance = new StorageFactory();

				return SelfInstance;
			}
		}


		#endregion

		#region Constructors
		private StorageFactory() { } // we shut our constructor
		#endregion


		public T CreateInstance<T>(params object[] args)
			where T : IStorageVMProducts
		{

			try
			{
				if (typeof(T) == typeof(OldFashionStorageVM))
					return (T)Activator.CreateInstance(typeof(OldFashionStorageVM), args);
			}
			catch (TargetInvocationException te)
			{
				throw te.InnerException;
			}
			
			return default(T);
		}

	}
}

