using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.core
{
    public class Publishers : AuditEntity<int>
    {
        public override int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }

        public string? Website { get; set; }

    }
}
