using System;
using NUnit;
using NUnit.Framework;
using Com.Bvinh.Linq;

namespace VendingMachineLibUnitTest
{

	[TestFixture]
	public class XifTest
	{

		[Description("IfTrue should be true only.")]
		[Test]
		public void TestIfTrueAlone()
		{
			bool res = false;
			bool falseShouldNeverHappen = false;

			true.IfTrue(() => { res = true; });
			true.IfFalse(() => { falseShouldNeverHappen = true; });

			Assert.IsTrue(res);
			Assert.IsFalse(falseShouldNeverHappen);
		}

		[Description("IFfalse should be false only.")]
		[Test]
		public void TestIfFalseAlone()
		{
			bool res = false;
			bool trueShouldNeverHappen = false;
			false.IfFalse(() => { res = true; });
			false.IfTrue(() => { res = true; });

			Assert.IsTrue(res);
			Assert.IsFalse(trueShouldNeverHappen);
		}

		// TODO : improve this description
		[Description("Different case ...")]
		[Test]
		public void TestIfTrueAndFalse()
		{
			bool resTrueCase = false;
			bool resFalseCase = false;

			bool shouldNeverHappenWithTrue = false;
			bool shouldNeverHappenWithFalse = false;

			bool resTrueAfterThatCase = false;
			bool resFalseAfterThatCase = false;


			// True Case

			var testResponse = true;

			testResponse
				.IfTrue(() => { resTrueCase = true; })
				.IfFalse(() => { shouldNeverHappenWithTrue = true; })
				.IfTrue(() => { resTrueAfterThatCase = true; });

			Assert.IsTrue(resTrueCase);
			Assert.IsFalse(shouldNeverHappenWithTrue);
			Assert.IsTrue(resTrueAfterThatCase);


			// False case
			testResponse = false;

			testResponse
				.IfFalse(() => { resFalseCase = true; })
				.IfTrue(() => { shouldNeverHappenWithFalse = true; })
				.IfFalse(() => { resFalseAfterThatCase = true; });

			Assert.IsTrue(resFalseCase);
			Assert.IsFalse(shouldNeverHappenWithFalse);
			Assert.IsTrue(resFalseAfterThatCase);

		}

	}
}

