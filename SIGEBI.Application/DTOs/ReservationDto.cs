
namespace SIGEBI.Application.DTOs
{
    public class ReservationDto //Mostrar info de reservacion
    {
        public int ReservationId { get; set; }
        public string UserName { get; set; } = "Sin nombre";
        public string BookTitle { get; set; } = "Sin titulo";
        public DateTime ReservationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string StatusName { get; set; } = "Sin estado";

    }
}
