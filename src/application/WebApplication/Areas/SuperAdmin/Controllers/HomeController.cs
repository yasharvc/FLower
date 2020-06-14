﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.SuperAdmin.Controllers
{
	[Area("SuperAdmin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}