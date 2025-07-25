using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.circulation
{
    [Table("Users")]
    public class User : AuditEntity<int>
    {
        [Key]
        [Column("UserId")]
        public override int Id { get; set; }
        public required string FullName { get; set; }
        public required string InstitutionalEmail { get; set; }
        public required string PasswordHash { get; set; } 
        public required string InstitutionalIdentifier { get; set; }
        public required string RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<Loans>? Loans { get; set; } = new List<Loans>();
        //public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
