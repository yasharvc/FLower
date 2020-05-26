using System;

namespace Core.Models
{
	public class StageHour: Model
	{
		public string StageTraceID { get; set; }
		public string UserId { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}