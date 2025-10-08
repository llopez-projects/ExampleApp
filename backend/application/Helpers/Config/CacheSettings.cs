namespace application.Helpers.Config
{
    public class CacheSettings
    {
        public int DefaultExpirationMinutes { get; set; } = 5;
        public int EmployeesExpirationMinutes { get; set; } = 10;
    }
}
