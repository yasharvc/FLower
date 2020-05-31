using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	public class PagesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Unauth() => Unauthorized("Not allowed");

		public IActionResult Test() => View();
	}
}