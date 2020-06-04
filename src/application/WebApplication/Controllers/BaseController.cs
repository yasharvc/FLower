using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication.Controllers
{
	public class BaseController : Controller
	{
		protected bool IsUserAuthenticated => User.Identity.IsAuthenticated;
		protected IServiceProvider ServiceProvider { get; set; }
		public BaseController(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
		protected T GetService<T>() => (T)ServiceProvider.GetService(typeof(T));
	}
}