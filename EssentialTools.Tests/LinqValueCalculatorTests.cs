using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

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
	}
}
