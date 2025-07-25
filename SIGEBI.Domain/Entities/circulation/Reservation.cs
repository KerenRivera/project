using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.core;
using SIGEBI.Domain.Entities.library;

namespace SIGEBI.Domain.Entities.circulation
{
    public class Reservation : AuditEntity<int>
    {
        [Key]
        [Column ("ReservationId")]
        public override int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime ReservationDate { get; set; }    
        public DateTime? ExpirationDate { get; set; }


        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string? ConfirmedBy { get; set; }


        public virtual Books Book { get; set; } // propiedad de navegacion
        public virtual User User { get; set; } // propiedad de navegacion
        //public virtual ReservationStatus ReservationStatus { get; set; } // propiedad de navegacion
    }
}
