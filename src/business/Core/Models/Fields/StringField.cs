﻿using Core.Enums;

namespace Core.Models.Fields
{
	public class StringField : Field
	{
		public int Length
		{
			get => GetPropertyValue<int>(nameof(Length));
			set => SetPropertyValue(nameof(Length), value, FieldTypeEnum.Integer);
		}

		public StringField(string name, string title) : this(name, title, 0) {
			Length = -1;
		}
		public StringField(string name, string title,uint length)
			: base(name, title)
		{
			FieldType = FieldTypeEnum.String;
			Length = (int)length;
		}

		public override object Default() => "";
		public override object Convert(object v) => System.Convert.ToString(v);
	}
}