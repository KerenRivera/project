using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.circulation
{
    public class ReservationStatus : AuditEntity<int>
    {
        [Key]
        [Column ("StatusId")]
        public override int Id {get; set; } 
        public required string StatusName { get; set; } 
        public required string Description { get; set; }
        
    }
}
