using SIGEBI.Application.DTOs.Base;

namespace SIGEBI.Application.DTOs
{
    public class CreateReservationRequestDto 
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public int StatusId { get; set; } = 1; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;

    }
}
