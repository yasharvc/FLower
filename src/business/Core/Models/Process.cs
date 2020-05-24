using Core.Interfaces.Models;

namespace Core.Models
{
	public class Process : Model, IName
	{
		public string Name { get; set; }
		public Stage Start { get; set; }
		public Stage End { get; set; }
		public string UniqueID { get; set; }
	}
}