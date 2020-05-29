using Core.Enums;

namespace Core.Models.Fields
{
	public abstract class CurrencyField : Field
	{
		protected decimal DecimalPart { get; set; }
		public string Symbol
		{
			get => GetPropertyValue<string>(nameof(Symbol));
			set => SetPropertyValue(nameof(Symbol), value, FieldTypeEnum.String);
		}
		public CurrencyField(string name, string label, FieldTypeEnum type,int decimalPoint = 2,string symbol = "$")
			: base(name, label)
		{
			DecimalPart = 1;
			for (int i = 0; i < decimalPoint; i++) DecimalPart /= 10m;
			Symbol = symbol;
			FieldType = type;
		}


		public override object Convert(object v)
		{
			decimal val = System.Convert.ToDecimal(v);
			val = val - val % DecimalPart;
			return val;
		}
	}
}