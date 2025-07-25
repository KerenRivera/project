using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities.library
{
    public class Authors : AuditEntity<int>
    {
        public override int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastNameId { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Nationality { get; set; }

        public virtual ICollection<BookAuthor>? BooksAuthors { get; set; } = new List<BookAuthor>();
    }
}
