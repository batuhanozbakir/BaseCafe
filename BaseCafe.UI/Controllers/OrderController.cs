using Microsoft.AspNetCore.Mvc;

namespace BaseCafe.UI.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
