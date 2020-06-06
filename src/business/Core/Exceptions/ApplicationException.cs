namespace Core.Exceptions
{
	public abstract class ApplicationException : System.Exception
	{
		public ApplicationException() { }
		public ApplicationException(string msg) : base(msg) { }
	}
}