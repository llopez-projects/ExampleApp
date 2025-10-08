namespace infrastructure.Logging.Models
{
    public class EndpointLog
    {
        public DateTime Timestamp { get; set; }
        public string Module { get; set; }
        public string HttpMethod { get; set; }
        public string Route { get; set; }
        public string User { get; set; }
        public string IP { get; set; }
        public int StatusCode { get; set; }
        public long DurationMs { get; set; }
        public long RequestSizeBytes { get; set; }
        public long ResponseSizeBytes { get; set; }
        public string QueryString { get; set; }
        public string CorrelationId { get; set; }
        public string RequestBody { get; set; }         
        public string ResponseBody { get; set; }       
    }
}
