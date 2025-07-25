
namespace SIGEBI.Application.DTOs.Base
{
    public abstract class BaseAuditDTo
    {
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty;
        public string? DeletedBy { get; set; } = string.Empty;
    }
}
