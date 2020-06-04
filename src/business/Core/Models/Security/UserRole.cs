namespace Core.Models.Security
{
	public class UserRole:Model
	{
		public User User { get; set; }
		public Role Role { get; set; }
	}
}