using Core.Enums;
using System.Linq;

namespace Core.Models.Fields
{
	public class PhoneField : Field
	{
		public int Length
		{
			get => GetPropertyValue<int>(nameof(Length));
			set => SetPropertyValue(nameof(Length), value, FieldTypeEnum.Integer);
		}
		public PhoneField(string name, string title)
			: base(name, title)
		{
			FieldType = FieldTypeEnum.String;
			Length = 11;
		}

		public override object Default() => "";
		public override object Convert(object v) => System.Convert.ToString(v);
		public override bool IsValid(object obj)
		{
			try
			{
				var str = (string)Convert(obj);
				if (str.StartsWith("+"))
					str = str.Substring(1);
				return str.All(c => char.IsDigit(c)) && str.Length <= Length;
			}
			catch
			{
				return false;
			}
		}
	}
}