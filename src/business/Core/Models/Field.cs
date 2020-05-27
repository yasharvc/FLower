using Core.Enums;
using Core.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
	public class Field : Model, IName
	{
		public string Name { get; set; }
		public FieldTypeEnum FieldType { get; set; }
		public object Value { get; set; }
		public IEnumerable<Field> SubFields { get; set; } = new List<Field>();
		public IEnumerable<Field> Properties { get; set; } = new List<Field>();

		protected T GetPropertyValue<T>(string name) => (T)Properties.Single(m => m.Name == name).Value;

		protected void SetPropertyValue<T>(string name, T value, FieldTypeEnum fieldType)
		{
			var property = Properties.SingleOrDefault(m => m.Name == name);
			if (property == null)
				(Properties as List<Field>).Add(new Field
				{
					Name = name,
					Value = value,
					FieldType = fieldType
				});
			else
				property.Value = value;
		}
	}
}