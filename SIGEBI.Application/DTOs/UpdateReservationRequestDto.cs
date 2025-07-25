
using SIGEBI.Application.DTOs.Base;

namespace SIGEBI.Application.DTOs
{
    public class UpdateReservationRequestDto 
    {
        public int ReservationId { get; set; }
        public int BookId { get; set; }  //new bookid
        //public int StatusId { get; set; }
        //public DateTime? ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7); 
        //public DateTime? ConfirmationDate { get; set; } 


    }
}
