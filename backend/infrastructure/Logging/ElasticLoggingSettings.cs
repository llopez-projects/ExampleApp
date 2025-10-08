namespace infrastructure.Logging
{
    public class ElasticLoggingSettings
    {
        public bool Enabled { get; set; }
        public string Uri { get; set; }
        public string IndexFormat { get; set; }
        public string ApplicationName { get; set; }
    }
}
