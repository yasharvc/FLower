using Core.Enums;

namespace Core.Models.Fields
{
	public class StringField : Field
	{
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

		public override object Default() => "";
		public override object Convert(object v) => System.Convert.ToString(v);
	}
}