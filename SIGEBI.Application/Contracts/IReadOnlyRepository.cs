using System.Linq.Expressions;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Contracts
{
    public interface IReadOnlyRepository<T> where T : class
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync(Expression<Func<T, bool>> filter); 
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
    }
}
