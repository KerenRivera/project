using System.Linq.Expressions;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Application.Contracts;

namespace SIGEBI.Application.Contracts.Repositories.Reservations
{
    public interface IReservationRepository
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync(Expression<Func<Reservation, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> filter);
        Task<OperationResult> AddAsync(Reservation entity);
        Task<OperationResult> UpdateAsync(Reservation entity);
        Task<OperationResult> DeleteAsync(Reservation entity);
    }
}
