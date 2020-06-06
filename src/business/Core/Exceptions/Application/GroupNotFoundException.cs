namespace Core.Exceptions.Application
{
	public class GroupNotFoundException : ApplicationException
	{
		public string GroupID { get; set; }
		public GroupNotFoundException() : base("") { }
		public GroupNotFoundException(string groupID) : base($"Given group not found[{groupID}]") { GroupID = groupID; }
	}
}