using EssentialTools.Models;
using System.Web.Mvc;

namespace EssentialTools.Controllers
{
	public class HomeController : Controller
	{
		private IValueCalculator calc;

		private Product[] products =
		{
			new Product { Name = "Kayak1", Category = "Watersports1", Price = 275M },
			new Product { Name = "Kayak2", Category = "Watersports2", Price = 270M },
			new Product { Name = "Kayak3", Category = "Watersports3", Price = 277M },
			new Product { Name = "Kayak4", Category = "Watersports3", Price = 274M },
		};

		public HomeController(IValueCalculator calcParam, IValueCalculator calcParam2)
		{
			calc = calcParam;
		}

		public ActionResult Index()
		{
			ShoppingCart shoppingCart = new ShoppingCart(calc);
			shoppingCart.Products = products;

			decimal totalValue = shoppingCart.CalculateProductTotal();

			return View(totalValue);
		}
	}
}