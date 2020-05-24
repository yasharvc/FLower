namespace Core.Interfaces.Models
{
	public interface ITraceID: IUniqueID
	{
		public string TraceID { get; set; }
	}
}