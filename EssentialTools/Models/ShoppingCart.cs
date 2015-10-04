using System.Collections.Generic;

namespace EssentialTools.Models
{
	public class ShoppingCart
	{
		private IValueCalculator calc;

		public IEnumerable<Product> Products { get; set; }

		public ShoppingCart(IValueCalculator calcParam)
		{
			calc = calcParam;
		}

		public decimal CalculateProductTotal()
		{
			return calc.ValueProducts(Products);
		}
	}
}