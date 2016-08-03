using System;
namespace Com.Bvinh.Linq
{

	// Bastien : for performance issue should not use this interface or tools. Because a common "if statement" is lighter than "If linq staement"
	// But sometimes for maintening issue, it is more readable to rea it like that.

	// TODO : Since I don't have time anymore, you should add IfFalseThrow and IfTrueThrow 

	public interface IIFTrue
	{
		IIfFalse IfFalse(Action a);
	}

	public interface IIfFalse
	{
		IIFTrue IfTrue(Action a);
	}

	internal class XIf : IIFTrue, IIfFalse
	{
		private readonly bool _currentResponse;

		internal XIf(bool rep)
		{
			_currentResponse = rep;
		}

		public IIfFalse IfFalse(Action a)
		{
			if (!_currentResponse)
				a();

			return this;
		}

		public IIFTrue IfTrue(Action a)
		{
			if (_currentResponse)
				a();

			return this;
		}
	}

	/// <summary>
	/// Extensions of normal type
	/// </summary>
	public static class XType
	{
		public static IIFTrue IfTrue(this bool response, Action a) => (new XIf(response)).IfTrue(a);
		public static IIfFalse IfFalse(this bool response, Action a) => (new XIf(response)).IfFalse(a);

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
	}

}

