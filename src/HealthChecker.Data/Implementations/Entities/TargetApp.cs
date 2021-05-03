namespace HealthChecker.Data.Implementations.Entities
{
    class TargetApp : EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int MonitoringInterval { get; set; }
    }
}
