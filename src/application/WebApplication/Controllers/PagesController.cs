using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	public class PagesController : Controller
	{
		public IActionResult Index(string id,int index)
		{
			ViewBag.index = index;
			return View("Index", id);
		}

		public IActionResult Unauth() => Unauthorized("Not allowed");

		public IActionResult Test() => View();
	}
}