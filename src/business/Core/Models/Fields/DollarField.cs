using Core.Enums;

namespace Core.Models.Fields
{
	public class DollarField : CurrencyField
	{
		public DollarField(string name, string label) : base(name, label, FieldTypeEnum.Dollar)
		{
		}
	}
}