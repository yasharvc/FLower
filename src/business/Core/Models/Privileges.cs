using System.Collections.Generic;

namespace Core.Models
{
	public class Privileges
	{
		public IEnumerable<string> AllowedGroups { get; set; }
		public IEnumerable<string> IncludedUsersIDs { get; set; }
		public IEnumerable<string> ExcludedUsersIDs { get; set; }
	}
}