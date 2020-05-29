using Core.Enums;

namespace Core.Models.Fields
{
	public class EuroField : CurrencyField
	{
		public EuroField(string name, string label) : base(name, label, FieldTypeEnum.Euro , symbol: "€")
		{
		}
	}
}