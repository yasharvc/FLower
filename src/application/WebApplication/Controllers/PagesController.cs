using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	[Authorize]
	public class PagesController : BaseController
	{
		public IActionResult Index(string id,int index)
		{
			ViewBag.index = index;
			return View("Index", id);
		}

		public IActionResult Unauth() => Unauthorized("Not allowed");

		public IActionResult Test() => View();

		[AllowAnonymous]
		public JsonResult Menu()
		{
			var menus = new List<MenuItem>();
			if (!IsUserAuthenticated)
			{
				menus.Add(new MenuItem {
					Icon = "login",
					IconColor="Primary",
					id="1",
					Link=$"/Security/{nameof(SecurityController.Login)}",
					Separator=true,
					Label="Log in"
				});
			}
			else
			{
				
			}
			return Json(menus);
		}
	}
}