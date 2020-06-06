namespace Core.Exceptions.Application
{
	public class RoleNotFoundException : ApplicationException
	{
		public string RoleID { get; set; }
		public RoleNotFoundException() : base("") { }
		public RoleNotFoundException(string roleID) : base($"Given role not found[{roleID}]") { RoleID = roleID; }
	}
}