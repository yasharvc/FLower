using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers;

namespace WebApplication.Areas.SuperAdmin.Controllers
{
	[Area("SuperAdmin")]
	public class GroupController : BaseController
	{
		public GroupController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}
	}
}
