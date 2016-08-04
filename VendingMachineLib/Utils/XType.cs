using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Functional.Maybe;

namespace Com.Bvinh.Linq
{

	// Bastien : for performance issue should not use this interface or tools. Because a common "if statement" is lighter than "If linq staement"
	// But sometimes for maintening issue, it is more readable to rea it like that.


	public interface IIFTrue
	{
		IIfFalse IfFalse(Action a);
		IIFTrueThrow IfTrueThrow<TException>(string message) where TException : Exception;
		IIFTrueThrow IfTrueThrow(Func<Exception> actionReturninExceptions);
		IIFFalseThrow IfFalseThrow<TException>(string message) where TException : Exception;
		IIFFalseThrow IfFalseThrow(Func<Exception> actionReturninExceptions);
	}

	public interface IIfFalse
	{
		IIFTrue IfTrue(Action a);
		IIFTrueThrow IfTrueThrow<TException>(string message) where TException : Exception;
		IIFTrueThrow IfTrueThrow(Func<Exception> actionReturninExceptions);
		IIFFalseThrow IfFalseThrow<TException>(string message) where TException : Exception;
		IIFFalseThrow IfFalseThrow(Func<Exception> actionReturninExceptions);
	}

	public interface IIFTrueThrow
	{
		IIFTrue IfTrue(Action a);
		IIfFalse IfFalse(Action a);
		IIFFalseThrow IfFalseThrow<TException>(string message) where TException : Exception;
		IIFFalseThrow IfFalseThrow( Func<Exception> actionReturninExceptions );
	}

	public interface IIFFalseThrow
	{
		IIFTrue IfTrue(Action a);
		IIfFalse IfFalse(Action a);
		IIFTrueThrow IfTrueThrow<TException>(string message) where TException : Exception;
		IIFTrueThrow IfTrueThrow(Func<Exception> actionReturninExceptions);
	}

	internal class XIf : IIFTrue, IIfFalse, IIFTrueThrow, IIFFalseThrow
	{
		private readonly bool _currentResponse;

		internal XIf(bool rep)
		{
			_currentResponse = rep;
		}


		#region False

		public IIfFalse IfFalse(Action a)
		{
			if (!_currentResponse)
				a();

			return this;
		}

		public IIFFalseThrow IfFalseThrow(Func<Exception> actionReturninExceptions)
		{
			// TODO : throw a special error in case he put actionReturninExceptions as null and it's negative
			if (!_currentResponse && actionReturninExceptions != null)
				throw actionReturninExceptions();

			return this;
		}

		public IIFFalseThrow IfFalseThrow<TException>(string message)
			where TException : Exception
		{
			
			if (!_currentResponse)
			{
				var maybeAGoodConstructorFromType = typeof(TException).GetConstructor(new[] { typeof(string) }).ToMaybe();

				if (maybeAGoodConstructorFromType.HasValue)
				{
					var exception = (TException)Activator.CreateInstance(typeof(TException), message);
					throw exception;
				}
				else
					throw new ArgumentException("TException must have message contructor");
				
			}

			return this;
		}

		#endregion

		#region True

		public IIFTrue IfTrue(Action a)
		{
			if (_currentResponse)
				a();

			return this;
		}

		public IIFTrueThrow IfTrueThrow(Func<Exception> actionReturninExceptions)
		{
			// TODO : throw a special error in case he put actionReturninExceptions as null and it's positive
			if (_currentResponse && actionReturninExceptions != null)
				throw actionReturninExceptions();

			return this;
		}

		public IIFTrueThrow IfTrueThrow<TException>(string message)
			where TException : Exception
		{

			if (_currentResponse)
			{
				var maybeAGoodConstructorFromType = typeof(TException).GetConstructor(new[] { typeof(string) }).ToMaybe();

				if (maybeAGoodConstructorFromType.HasValue)
				{
					var exception = (TException)Activator.CreateInstance(typeof(TException), message);
					throw exception;
				}
				else
					throw new ArgumentException("TException must have message contructor");
			}

			return this;
		}


		#endregion
	}

	/// <summary>
	/// Extensions of normal type
	/// </summary>
	public static class XType
	{
		public static IIFTrue IfTrue(this bool response, Action a) => (new XIf(response)).IfTrue(a);
		public static IIfFalse IfFalse(this bool response, Action a) => (new XIf(response)).IfFalse(a);

		public static IIFTrueThrow IfTrueThrow<TException>(this bool response, string message)
			where TException : Exception => (new XIf(response)).IfTrueThrow<TException>(message);

		public static IIFTrueThrow IfTrueThrow(this bool response, Func<Exception> actionReturninExceptions)
		=> (new XIf(response)).IfTrueThrow(actionReturninExceptions);

		public static IIFFalseThrow IfFalseThrow<TException>(this bool response, string message)
			where TException : Exception => (new XIf(response)).IfFalseThrow<TException>(message);

		public static IIFFalseThrow IfFalseThrow(this bool response, Func<Exception> actionReturninExceptions)
		=> (new XIf(response)).IfFalseThrow(actionReturninExceptions);

		/// <summary>
		/// Make a action on true or false case
		/// </summary>
		/// <param name="actionOnTrue">Action to run on true.</param>
		/// <param name="actionOnFalse">Action  to run on false.</param>
		public static void If(this bool response, Action actionOnTrue, Action actionOnFalse)
		{
			if (response && actionOnTrue != null)
				actionOnTrue();
			else if (!response && actionOnFalse != null)
				actionOnFalse();
		}


		/// <summary>
		/// Makes a copy from the object.
		/// Doesn't copy the reference memory, only data.
		/// <reference>http://extensionmethod.net/csharp/object/clone-t</reference>
		/// </summary>
		/// <typeparam name="T">Type of the return object.</typeparam>
		/// <param name="item">Object to be copied.</param>
		/// <returns>Returns the copied object.</returns>
		public static T Clone<T>(this object item)
		{
			if (item != null)
			{
				var formatter = new BinaryFormatter();
				var stream = new MemoryStream();

				formatter.Serialize(stream, item);
				stream.Seek(0, SeekOrigin.Begin);

				var result = (T)formatter.Deserialize(stream);

				stream.Close();

				return result;
			}

			return default(T);
		}
	}

}

