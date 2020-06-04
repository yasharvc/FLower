namespace Core.Models.Security
{
	public class AuthenticationSettings
	{
		public string Issuer { get; set; }
		public bool AllowRefresh { get; set; }
		public int ExpireMinutes { get; set; }
		public bool IsPersistent { get; set; }
		public string LogoutPath { get; set; }
		public string LoginPath { get; set; }
		public string AccessDeniedPath { get; set; }
	}
}