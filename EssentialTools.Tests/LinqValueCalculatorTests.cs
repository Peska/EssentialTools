using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;
using System;

namespace EssentialTools.Tests
{
	[TestClass]
	public class LinqValueCalculatorTests
	{
		private Product[] products =
		{
			new Product { Name = "Kayak1", Category = "Watersports1", Price = 275M },
			new Product { Name = "Kayak2", Category = "Watersports2", Price = 270M },
			new Product { Name = "Kayak3", Category = "Watersports3", Price = 277M },
			new Product { Name = "Kayak4", Category = "Watersports3", Price = 274M },
		};

		private Product[] CreateProduct(decimal value)
		{
			return new[] { new Product { Price = value } };
		}

		[TestMethod]
		public void SumProductsCorrectly()
		{
			// Arrange
			Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
			mock.Setup(x => x.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(x => x);

			IValueCalculator target = new LinqValueCalculator(mock.Object);
			decimal goalTotal = products.Sum(x => x.Price);

			// Act
			decimal result = target.ValueProducts(products);

			// Assert
			Assert.AreEqual(goalTotal, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PassThroughVariableDiscount()
		{
			// Arrange
			Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();

			mock.Setup(x => x.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(x => x);
			mock.Setup(x => x.ApplyDiscount(It.Is<decimal>(y => y == 0))).Throws<ArgumentOutOfRangeException>();
			mock.Setup(x => x.ApplyDiscount(It.Is<decimal>(y => y > 100))).Returns<decimal>(x => x * 0.9M);
			mock.Setup(x => x.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).Returns<decimal>(x => x - 5);

			IValueCalculator target = new LinqValueCalculator(mock.Object);

			// Act
			decimal fiveDollarDiscount = target.ValueProducts(CreateProduct(5));
			decimal tenDollarDiscount = target.ValueProducts(CreateProduct(10));
			decimal fiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
			decimal hundredDollarDiscount = target.ValueProducts(CreateProduct(100));
			decimal fiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));

			// Assert
			Assert.AreEqual(5, fiveDollarDiscount, "$5 Fail");
			Assert.AreEqual(5, tenDollarDiscount, "$10 Fail");
			Assert.AreEqual(45, fiftyDollarDiscount, "$50 Fail");
			Assert.AreEqual(95, hundredDollarDiscount, "$100 Fail");
			Assert.AreEqual(450, fiveHundredDollarDiscount, "$500 Fail");

			target.ValueProducts(CreateProduct(0));
		}
	}
}
