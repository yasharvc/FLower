namespace Core.Models.Fields
{
	public class GregorianDateField : Field
	{
		public GregorianDateField(string name, string title)
			: base(name, title)
		{
			FieldType = Enums.FieldTypeEnum.GregorianDate;
		}
		public override bool IsValid(object obj) => base.IsValid(obj);
		public override object Default() => 0;
		public override object Convert(object v) => System.Convert.ToDateTime(v);
	}
}