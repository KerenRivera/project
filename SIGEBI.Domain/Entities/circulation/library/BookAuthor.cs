using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.library
{
    public class BookAuthor : AuditEntity<int>
    {
        public override int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public virtual Books? Book { get; set; }
        public virtual Authors? Author { get; set; }
  
    }
}
