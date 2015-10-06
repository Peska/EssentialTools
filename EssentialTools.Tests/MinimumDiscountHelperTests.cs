using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;

namespace EssentialTools.Tests
{
	[TestClass]
	public class MinimumDiscountHelperTests
	{
		private IDiscountHelper GetTestObject()
		{
			return new MinimumDiscountHelper();
		}

		[TestMethod]
		public void DiscountAbove100()
		{
			// Arrange
			IDiscountHelper target = GetTestObject();
			decimal total = 200;

			// Act
			decimal discountedTotal = target.ApplyDiscount(total);

			// Assert
			Assert.AreEqual(total * 0.9M, discountedTotal);
		}

		[TestMethod]
		public void DiscountBetween10And100()
		{
			// Arrange
			IDiscountHelper target = GetTestObject();

			// Act
			decimal tenDollarDiscount = target.ApplyDiscount(10);
			decimal fiftyDollarDiscount = target.ApplyDiscount(50);
			decimal hundredDollarDiscount = target.ApplyDiscount(100);

			// Assert
			Assert.AreEqual(5, tenDollarDiscount, "$10 discount is wrong");
			Assert.AreEqual(45, fiftyDollarDiscount, "$50 discount is wrong");
			Assert.AreEqual(95, hundredDollarDiscount, "$100 discount is wrong");
		}

		[TestMethod]
		public void DiscountLessThan10()
		{
			// Arrange
			IDiscountHelper target = GetTestObject();

			// Act
			decimal discount5 = target.ApplyDiscount(5);
			decimal discount0 = target.ApplyDiscount(0);

			// Assert
			Assert.AreEqual(5, discount5);
			Assert.AreEqual(0, discount0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DiscountNegativeTotal()
		{
			// Arrange
			IDiscountHelper target = GetTestObject();

			// Act
			decimal discount5 = target.ApplyDiscount(-1);
		}
	}
}
