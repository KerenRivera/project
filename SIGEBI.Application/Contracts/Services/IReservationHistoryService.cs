using System.Linq.Expressions;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Validations;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.Application.Contracts.Services
{
    public interface IReservationHistoryService
    {
        Task<OperationResult> GetAllHistoriesAsync(Expression<Func<Reservation, bool>> filter);
        Task<OperationResult> GetHistoryByIdAsync(int id);

    }
}
