
namespace SIGEBI.Application.DTOs
{
    public class CreateReservationResponseDto
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? BookTitle { get; set; } // Solo el título, no el objeto entero
        public string? UserName { get; set; } 
        public string? Status { get; set; }   
    }
}
