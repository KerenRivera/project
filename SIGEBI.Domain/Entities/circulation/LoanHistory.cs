using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Domain.Entities.circulation
{
    public class LoanHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public required string FinalStatus { get; set; }
    }
}
