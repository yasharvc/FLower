namespace Core.Models.Fields
{
	public class PositiveIntegerField : IntegerField
	{
		public PositiveIntegerField(string name, string title) : base(name, title, 0, int.MaxValue) { }
	}
}