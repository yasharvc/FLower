using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_logger = logger;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var authProps = GetService<AuthenticationProperties>();
			return View(authProps);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
