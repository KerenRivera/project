using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.core
{
    public class SpecialMaterials : AuditEntity<int>
    {
        public override int Id { get; set; }
        public required string Tittle { get; set; } 
        public required string Author { get; set; } 
        public string? Description { get; set; } 
        public DateTime RequestDate { get; set; } 
        public int UserId { get; set; } 
        public string? ApprovalStatus { get; set; } 
        public DateTime AdquisitionDate { get; set; }
      
    }
}
