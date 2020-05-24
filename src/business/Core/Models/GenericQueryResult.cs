using System.Collections.Generic;

namespace Core.Models
{
	public class GenericQueryResult<T> : Model
	{
		public IEnumerable<T> Result { get; set; }
		public long TotalRecordCount { get; set; }
	}
}