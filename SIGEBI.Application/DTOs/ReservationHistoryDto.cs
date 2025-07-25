
namespace SIGEBI.Application.DTOs
{
    public class ReservationHistoryDto : Base.BaseAuditDTo
    {
        public int HistoryId { get; set; }
        public int ReservationId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? StatusName { get; set; }
        public string? BookTitle { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; } 
    }
}
