using Core.Interfaces.Services;
using Core.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	[Authorize]
	public class PagesController : BaseController
	{
		public PagesController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public IActionResult Index(string id,int index)
		{
			ViewBag.index = index;
			//return View("Index", id);
			return Content("Congroooo");
		}

		public IActionResult Unauth() => Unauthorized("Not allowed");

		public IActionResult Test() => View();

		[AllowAnonymous]
		public async Task<JsonResult> Menu()
		{
			var menus = new List<Menu>();
			if (!IsUserAuthenticated)
			{
				menus.Add(new Menu {
					Icon = "login",
					IconColor="Primary",
					_id="1",
					Link=$"/Security/{nameof(SecurityController.Login)}",
					Separator=true,
					Label="Log in"
				});
			}
			else
			{
				var index = 1;
				var service = GetService<IUserMenuService>();
				var userID = HttpContext.User.Claims.Single(m => m.Type == ClaimTypes.GivenName).Value;
				await foreach (var item in service.GetUserMenu(userID))
				{
					item._id = $"{index++}";
					menus.Add(item);
				}
				menus.Add(new Menu
				{
					Icon = "logout",
					IconColor = "red",
					_id = $"{index++}",
					Link = $"/Security/{nameof(SecurityController.Logout)}",
					Separator = true,
					Label = "Log out"
				});
			}
			return Json(menus);
		}
	}
}