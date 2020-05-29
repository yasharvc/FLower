namespace Core.Models.Fields
{
	public class PercentField : FloatField
	{
		public PercentField(string name, string title) : base(name, title, 0, 100) { }
	}
}