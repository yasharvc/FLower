using Core.Enums;

namespace Core.Models.Fields
{
	public class BooleanField:Field
	{
		public BooleanField(string name, string title)
			: base(name, title)
		{
			FieldType = FieldTypeEnum.Boolean;
		}
		public override bool IsValid(object obj) => base.IsValid(obj);
		public override object Default() => 0.0f;
		public override object Convert(object v) => System.Convert.ToBoolean(v);
	}
}