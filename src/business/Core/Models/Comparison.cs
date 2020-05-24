using Core.Enums;
using System.Collections.Generic;

namespace Core.Models
{
	public class Comparison : Model
	{
		public ComparisonOperation Operation { get; set; }
		public string FieldName { get; set; }
		public BaseColumnType ColumnType { get; set; }
		public List<string> Values { get; set; }

		public string GetFirstValue() => Values[0];
	}
}