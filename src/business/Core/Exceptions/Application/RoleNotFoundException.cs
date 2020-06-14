namespace Core.Exceptions.Application
{
	public class RoleNotFoundException : ApplicationException
	{
		public string RoleID { get; set; }

		public override int Code => 3;

		public RoleNotFoundException() : base("") { }
		public RoleNotFoundException(string roleID) : base($"Given role not found[{roleID}]") { RoleID = roleID; }
	}
}