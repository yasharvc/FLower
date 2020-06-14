namespace Core.Exceptions.Application
{
	public class UserNotFoundException : ApplicationException
	{
		public string UserID { get; set; }

		public override int Code => 4;

		public UserNotFoundException():base("") { }
		public UserNotFoundException(string userID) : base($"Given user not found[{userID}]") { UserID = userID; }
	}
}