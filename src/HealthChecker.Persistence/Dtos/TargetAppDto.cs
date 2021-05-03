namespace HealthChecker.Persistence.Dtos
{
    public class TargetAppDto : DtoBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public string UserId { get; set; }
    }
}
