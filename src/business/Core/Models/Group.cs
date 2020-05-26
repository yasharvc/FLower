using Core.Interfaces.Models;

namespace Core.Models
{
	public class Group : Model, IName
	{
		public string Name { get; set; }
	}
}