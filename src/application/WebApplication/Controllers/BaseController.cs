﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
	public class BaseController : Controller
	{
		protected bool IsUserAuthenticated => User.Identity.IsAuthenticated;
	}
}
