using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EssentialTools.Models
{
	public class LinqValueCalculator : IValueCalculator
	{
		private IDiscountHelper discounter;
		private static int counter;

		public LinqValueCalculator(IDiscountHelper discountParam)
		{
			discounter = discountParam;
			Debug.WriteLine(string.Format("Instance {0} created", ++counter));
		}

		public decimal ValueProducts(IEnumerable<Product> products)
		{
			return discounter.ApplyDiscount(products.Sum(x => x.Price));
		}
	}
}