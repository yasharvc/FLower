using Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Security
{
	public class Role : Model,IName
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
