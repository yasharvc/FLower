namespace Core.Models
{
	public class ManipulationPrivileges
	{
		public Privileges View { get; set; }
		public Privileges Manipulate { get; set; }
		public Privileges DataEntry { get; set; }
	}
}