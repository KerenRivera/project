using System.Linq.Expressions;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;
namespace SIGEBI.Application.Contracts.Repositories.Reservations
{
    public interface IReservationStatusesRepository
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync(Expression<Func<ReservationStatus, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<ReservationStatus, bool>> filter);
        Task <string> GetStatusNameByIdAsync(int id);
    }
}
