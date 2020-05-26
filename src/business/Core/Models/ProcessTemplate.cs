using System;
using System.Collections.Generic;

namespace Core.Models
{
	public class ProcessTemplate : Model
	{
		public Process Template { get; set; }
		public string CreatorID { get; set; }
		public DateTime CreationDateTime { get; set; }
		public bool IsActive { get; set; }
		public List<ProcessTemplate> History { get; set; }
	}
}