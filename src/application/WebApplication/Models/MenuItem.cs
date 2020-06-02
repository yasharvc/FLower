using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
	public class MenuItem
	{
		public string id { get; set; }
		public string Icon { get; set; }
		public string IconColor { get; set; }
		public string Label { get; set; }
		public bool Separator { get; set; }
		public string Link { get; set; }
	}
}