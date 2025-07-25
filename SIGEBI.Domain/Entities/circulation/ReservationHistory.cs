using SIGEBI.Domain.Entities.library;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.circulation
{
    public class ReservationHistory  // Guarga un registro de reservas ya finalizadas. Es una copia de la transaccion finalizada.
    {
        public int ReservationHistoryId { get; set; }
        public int ReservationId { get; set; }
        public int BookId { get; set; } 
        public int UserId { get; set; }
        public int StatusId  { get; set; } 
        public DateTime ReservationDate { get; set; } 
        public DateTime ExpirationDate { get; set; } 

        public virtual Books? Book { get; set; } // Relacion con el libro
        public virtual User? User { get; set; } // Relacion con el usuario
        public virtual ReservationStatus? ReservationStatus { get; set; } // Relacion con el estado de la reserva



    }
}
