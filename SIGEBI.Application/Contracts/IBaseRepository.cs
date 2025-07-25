using System.Linq.Expressions;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Contracts
{
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {        
        Task<OperationResult> AddAsync(TEntity entity);
        Task<OperationResult> UpdateAsync(TEntity entity);
        Task<OperationResult> DeleteAsync(TEntity entity);
    }
}
