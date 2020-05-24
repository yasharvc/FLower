using Core.Enums;
using Core.Interfaces.Models;
using System.Collections.Generic;

namespace Core.Models
{
	public class StageResult: Model, IName
	{
		public string Name { get; set; }
		public StageResult Previous { get; set; }
		public List<StageResult> Next { get; set; }
		public string UniqueID { get; set; }
		public string TraceID { get; set; }
		public StatusEnum Status { get; set; }
	}
}