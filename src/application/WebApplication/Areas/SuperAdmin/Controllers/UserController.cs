using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication.Controllers;

namespace WebApplication.Areas.SuperAdmin.Controllers
{
	[Area("SuperAdmin")]
	public class UserController : BaseController
	{
		public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		[HttpGet]
		public IActionResult Create(string id)
		{
			return View(nameof(Create), id);
		}
		[HttpPost]
		public async Task<IActionResult> Create(string username, string password)
		{
			var userSrv = GetService<IUserService>();
			try
			{
				await userSrv.Create(new Core.Models.User
				{
					Password = password,
					Username = username
				});
				return Json(new { result = true });
			}
			catch(Core.Exceptions.ApplicationException ex) {
				return Json(new { result = false, errors = new[] { ex.Code } });
			}
		}
	}
}
