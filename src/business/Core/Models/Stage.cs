using Core.Interfaces.Models;
using System.Collections.Generic;

namespace Core.Models
{
	public class Stage: Model, IName
	{
		public string Name { get; set; }
		public Stage Previous { get; set; }
		public List<Stage> Next { get; set; }
		public string UnqueID { get; set; }
	}
}