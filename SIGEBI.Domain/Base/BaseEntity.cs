
namespace SIGEBI.Domain.Base
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}
