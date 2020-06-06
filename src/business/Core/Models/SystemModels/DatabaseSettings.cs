namespace Core.Models.SystemModels
{
    public class DatabaseSettings : Model
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}