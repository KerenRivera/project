using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.library;

namespace SIGEBI.Domain.Entities.circulation
{
    public class Loans : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public required string LoanStatus { get; set; } 
        
        public virtual Books? Book { get; set; }
        public virtual User? User { get; set; } 

        public virtual ICollection<LoanHistory> LoanHistories { get; set; } = new List<LoanHistory>();
    }
}
