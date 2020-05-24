using Core.Interfaces.Models;
using System;

namespace Core.Models
{
	public class ProcessResult : Model, ITraceID, IName
	{
		public string Name { get; set; }
		public StageResult Start { get; set; }
		public StageResult End { get; set; }
		public string UniqueID { get; set; }
		public string TraceID { get; set; }
		public User Creator { get; set; }
		public DateTime CreationTime { get; set; }
		public User Worker { get; set; }
		public DateTime? LastModified { get; set; }
	}
}