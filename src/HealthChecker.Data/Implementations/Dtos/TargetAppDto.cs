namespace HealthChecker.Data.Implementations.Dtos
{
    class TargetAppDto : DtoBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int MonitoringInterval { get; set; }
    }
}
