using System;

namespace EssentialTools.Models
{
	public interface IDiscountHelper
	{
		decimal ApplyDiscount(decimal totalParam);
	}

	public class DefaulDiscountHelper : IDiscountHelper
	{
		public decimal DiscountSize { get; set; }

		public DefaulDiscountHelper(decimal discountParam)
		{
			DiscountSize = discountParam;
		}

		public decimal ApplyDiscount(decimal totalParam)
		{
			return (totalParam - (DiscountSize / 100m * totalParam));
		}
	}

	public class FlexibleDiscountHelper : IDiscountHelper
	{
		public decimal ApplyDiscount(decimal totalParam)
		{
			decimal discount = totalParam > 100 ? 70 : 25;

			return (totalParam - (discount / 100m * totalParam));
		}
	}
}