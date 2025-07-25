using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.circulation
{
    public class Sanctions : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int UserId { get; set; }
        public required string SanctionType { get; set; } 
        public decimal FineAmount { get; set; } 
        public DateTime ImpositionDate { get; set; } 

        public DateTime? DueDate { get; set; } 
        public DateTime? PaymentDate { get; set; } 
        public required string Reason { get; set; }
        public required string SanctionStatus { get; set; } 

        public required virtual User User { get; set; } 
    }
}
