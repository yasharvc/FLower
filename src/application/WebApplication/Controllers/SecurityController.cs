using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
	[AllowAnonymous]
	public class SecurityController : BaseController
	{
		public SecurityController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		[HttpGet]
		public IActionResult Login(string index)
		{
			return View("login", index);
		}

		[HttpPost]
		public async Task<IActionResult> Login(string username,string password)
		{
			await Authenticate(new List<Claim>
			{
				new Claim(ClaimTypes.Role,"ADMIN")
			});
			return Json(new { result = true });
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return Content("<script>window.location='/';</script>");
		}

		protected async Task Authenticate(IEnumerable<Claim> claims)
		{
			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = false,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false,
				IssuedUtc = DateTimeOffset.UtcNow,
			};
			await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
		}
	}
}
