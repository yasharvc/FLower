using Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models.Fields
{
	public class StringField : Field
	{
		public string Title
		{
			get => GetPropertyValue<string>(nameof(Title));
			set => SetPropertyValue(nameof(Title), value, FieldTypeEnum.String);
		}

		public int Length
		{
			get => GetPropertyValue<int>(nameof(Length));
			set => SetPropertyValue(nameof(Length), value, FieldTypeEnum.Integer);
		}

		public StringField(string name, string title)
		{
			Name = name;
			FieldType = FieldTypeEnum.String;
			Title = title;
			Length = -1;
		}
		public StringField(string name, string title,uint length)
		{
			Name = name;
			FieldType = FieldTypeEnum.String;
			Title = title;
			Length = (int)length;
		}
	}
}