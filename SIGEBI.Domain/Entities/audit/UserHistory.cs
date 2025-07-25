namespace SIGEBI.Domain.Entities.audit
{
    public class UserHistory
    {
        public int Logid { get; set; }
        public int UserId { get; set; }
        public required string EnteredEmail { get; set; } 
        public DateTime AttempDate { get; set; }
        public required string IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public bool IsSuccessful { get; set; }
        public string? FailureReason { get; set; }
        public required string ObteinedRole { get; set; }
    }
}
