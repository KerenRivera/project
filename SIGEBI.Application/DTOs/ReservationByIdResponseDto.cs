
namespace SIGEBI.Application.DTOs
{
    internal class ReservationByIdResponseDto : Base.DtoBase
    {
        public int ReservationId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty; 
        public DateTime ReservationDate { get; set; }
        public DateTime? ExpirationDate { get; set; } 
        public DateTime? ConfirmedBy { get; set; } // Indicates if the reservation is confirmed
    }
}
