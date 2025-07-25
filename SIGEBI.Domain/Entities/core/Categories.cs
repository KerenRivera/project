using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.core
{
    public class Categories : AuditEntity<int>
    {
        public override int Id { get; set; }
        public required string CategoryName { get; set; }
        public string? Description { get; set; }

    }
    
}
