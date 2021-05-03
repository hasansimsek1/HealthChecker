namespace HealthChecker.Persistence.Dtos
{
    class CheckResultDto : DtoBase
    {
        public string UserId { get; set; }
        public string TargetAppId { get; set; }
        public int HttpStatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string StatusExplanation { get; set; }
        public string LogFilePath { get; set; }
    }
}
