namespace Core.Models.Fields
{
	public class PersianRialField : CurrencyField
	{
		public PersianRialField(string name, string label) : base(name, label, Enums.FieldTypeEnum.PersianRial, symbol: "ريال")
		{
		}
	}
}