using Core.Enums;
using Core.Interfaces.Models;
using System;
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
		public string Title
		{
			get => GetPropertyValue<string>(nameof(Title));
			set => SetPropertyValue(nameof(Title), value, FieldTypeEnum.String);
		}

		public Field() { }

		public Field(string name,string title)
		{
			Name = name;
			Title = title;
		}

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

		public virtual bool IsValid(object obj)
		{
			try
			{
				Convert(obj);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public virtual object Default() => null;

		public virtual object Convert(object v) => v;
	}
}