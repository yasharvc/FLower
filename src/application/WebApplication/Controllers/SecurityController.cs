using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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
		public IActionResult Login(string username,string password)
		{
			//Authenticate(new List<Claim>
			//{
			//	new Claim(ClaimTypes.Role,"ADMIN")
			//});
			return Json(new { result = true });
		}

		private void Authenticate(IEnumerable<Claim> claims)
		{
			//var claims = new List<Claim>
			//{
			//	new Claim(ClaimTypes.Name, ""),
			//	new Claim("FullName", ""),
			//	new Claim(ClaimTypes.Role, "Administrator")
			//};
			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = false,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = false,
				IssuedUtc = DateTimeOffset.UtcNow,
			};
			HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
		}
	}
}
