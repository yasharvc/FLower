using System.Linq;

namespace Core.Models
{
	public class GenericQueryResult<T> : Model
	{
		public IQueryable<T> Result { get; set; }
		public long TotalRecordCount { get; set; }
	}
}