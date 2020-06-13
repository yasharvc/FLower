namespace Core.Models.Security
{
	public class Menu : Model
	{
		public string Icon { get; set; }
		public string IconColor { get; set; }
		public string Label { get; set; }
		public bool Separator { get; set; }
		public string Link { get; set; }
	}
}