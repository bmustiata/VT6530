using System;
using NUnit.Framework;

namespace DOR.TandemScreens.UnitTests
{
	[TestFixture]
	public class PhoneNumTest
	{
		public PhoneNumTest()
		{
		}

		[Test]
		public void NumericParseTest()
		{
			PhoneNumber ph = new PhoneNumber(9991234567);
			Assert.AreEqual(999, ph.AreaCode, "Area code parsed wrong");
			Assert.AreEqual(123, ph.Prefix, "Prefix parsed wrong");
			Assert.AreEqual(4567, ph.Suffix, "Suffix parsed wrong");
		}

		[Test]
		public void StringParseTest()
		{
			PhoneNumber ph = new PhoneNumber("9991234567");
			Assert.AreEqual(999, ph.AreaCode, "Area code parsed wrong");
			Assert.AreEqual(123, ph.Prefix, "Prefix parsed wrong");
			Assert.AreEqual(4567, ph.Suffix, "Suffix parsed wrong");			

			ph = new PhoneNumber("999-123-4567");
			Assert.AreEqual(999, ph.AreaCode, "Area code parsed wrong");
			Assert.AreEqual(123, ph.Prefix, "Prefix parsed wrong");
			Assert.AreEqual(4567, ph.Suffix, "Suffix parsed wrong");			

			ph = new PhoneNumber("(999)123-4567");
			Assert.AreEqual(999, ph.AreaCode, "Area code parsed wrong");
			Assert.AreEqual(123, ph.Prefix, "Prefix parsed wrong");
			Assert.AreEqual(4567, ph.Suffix, "Suffix parsed wrong");			

			ph = new PhoneNumber("(999) 123-4567");
			Assert.AreEqual(999, ph.AreaCode, "Area code parsed wrong");
			Assert.AreEqual(123, ph.Prefix, "Prefix parsed wrong");
			Assert.AreEqual(4567, ph.Suffix, "Suffix parsed wrong");			
		}

		[Test]
		public void FormatTest()
		{
			PhoneNumber ph = new PhoneNumber(999, 123, 4567);
			Assert.AreEqual("(999) 123-4567", ph.ToString(), "Incorrect format");

			ph = new PhoneNumber(999, 23, 567);
			Assert.AreEqual("(999) 023-0567", ph.ToString(), "Incorrect format (zeros)");
		}

		[Test]
		public void ToIntTest()
		{
			PhoneNumber ph = new PhoneNumber(999, 123, 4567);
			Assert.AreEqual(9991234567, ph.ToInt(), "Incorrect int conversion");
		}
	}
}
