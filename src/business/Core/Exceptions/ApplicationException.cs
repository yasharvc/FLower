namespace Core.Exceptions
{
	public abstract class ApplicationException : System.Exception
	{
		public abstract int Code { get; }
		public ApplicationException() { }
		public ApplicationException(string msg) : base(msg) { }
	}
}