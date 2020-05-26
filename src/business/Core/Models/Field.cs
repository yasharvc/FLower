using Core.Enums;
using Core.Interfaces.Models;
using System.Collections.Generic;

namespace Core.Models
{
	public class Field : Model, IName
	{
		public string Name { get; set; }
		public FieldTypeEnum FieldType { get; set; }
		public object Value { get; set; }
		public IEnumerable<Field> Properties { get; set; }
	}
}