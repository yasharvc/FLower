﻿using System.Collections.Generic;

namespace Core.Models
{
	public class User: Model
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Groups { get; set; }
	}
}