
namespace SIGEBI.Domain.Base
{
    public abstract class AuditEntity<TKey> : BaseEntity<TKey>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public  string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        //public string? Notes { get; set; }

    }
}
