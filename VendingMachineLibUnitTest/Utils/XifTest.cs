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


		[Description("IfTrueThrow throw an exception corretly without the multiple case.")]
		[Test]
		public void TestIfTrueThrowMethodsAreWorking()
		{
			const string LOCAL_CONST_GOOD_MESSAGE = "The life is superb, and it's sunny, but go catch pokemon buddy ...";
			const string LOCAL_CONST_NOT_GOOD_AT_ALL = "What the heck you have this message, redevelp again your methods, stuoid guy";


			// First methods wih IfTrueThrow(Func<Exception> func);

			Assert.Throws<AnyException>(() =>
			{
				true.IfTrueThrow(() => new AnyException(LOCAL_CONST_GOOD_MESSAGE));
			}, LOCAL_CONST_GOOD_MESSAGE);

			Assert.DoesNotThrow(() =>
			{
				true.IfFalseThrow(() => new AnyException(LOCAL_CONST_NOT_GOOD_AT_ALL));
				false.IfTrueThrow(() => new AnyException(LOCAL_CONST_NOT_GOOD_AT_ALL));
			});


			// Seconds methods with ifTrueThrow<Exception>(string message) 

			Assert.Throws<AnyException>(() => { true.IfTrueThrow<AnyException>(LOCAL_CONST_GOOD_MESSAGE); }, LOCAL_CONST_GOOD_MESSAGE);
			Assert.DoesNotThrow(() =>
			{
				true.IfFalseThrow<AnyException>(LOCAL_CONST_NOT_GOOD_AT_ALL);
				false.IfTrueThrow<AnyException>(LOCAL_CONST_NOT_GOOD_AT_ALL);
			});

		}

		[Description("IfFalseThrow throw an exception corretly without the multiple case.")]
		[Test]
		public void TestIfFalseThrowMethodsAreWorking()
		{
			const string LOCAL_CONST_GOOD_MESSAGE = "The life is superb, and it's sunny, but go catch pokemon buddy ...";
			const string LOCAL_CONST_NOT_GOOD_AT_ALL = "What the heck you have this message, redevelp again your methods, stuoid guy";



			// First methods wih IfFalseThrow(Func<Exception> func);

			Assert.Throws<AnyException>(() =>
			{
				false.IfFalseThrow(() => new AnyException(LOCAL_CONST_GOOD_MESSAGE));
			}, LOCAL_CONST_GOOD_MESSAGE);

			Assert.DoesNotThrow(() =>
			{
				false.IfTrueThrow(() => new AnyException(LOCAL_CONST_NOT_GOOD_AT_ALL));
				true.IfFalseThrow(() => new AnyException(LOCAL_CONST_NOT_GOOD_AT_ALL));
			});


			// Seconds methods with ifTrueThrow<Exception>(string message) 

			Assert.Throws<AnyException>(() => { false.IfFalseThrow<AnyException>(LOCAL_CONST_GOOD_MESSAGE); }, LOCAL_CONST_GOOD_MESSAGE);
			Assert.DoesNotThrow(() =>
			{
				false.IfTrueThrow<AnyException>(LOCAL_CONST_NOT_GOOD_AT_ALL);
				true.IfFalseThrow<AnyException>(LOCAL_CONST_NOT_GOOD_AT_ALL);
			});

		}
	}
}

