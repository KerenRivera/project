
namespace SIGEBI.Application.DTOs
{
    public class ReservationUpdateResponseDto : Base.DtoBase // response de update reservation
    {
        public int ReservationId { get; set; }
        public int BookId { get; set; } 
        public string StatusName { get; set; } = string.Empty; // Nombre del estado de la reserva
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Fecha de actualizacion de la reserva

    }
}
