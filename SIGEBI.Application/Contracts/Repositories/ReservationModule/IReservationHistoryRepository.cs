using System.Linq.Expressions;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.Application.Contracts.Repositories.Reservations
{
    public interface IReservationHistoryRepository 
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<bool> ExistsAsync(Expression<Func<ReservationHistory, bool>> filter);
    }
}
