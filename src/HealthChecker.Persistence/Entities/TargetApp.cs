namespace HealthChecker.Persistence.Entities
{
    public class TargetApp : EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public string UserId { get; set; }
    }
}
