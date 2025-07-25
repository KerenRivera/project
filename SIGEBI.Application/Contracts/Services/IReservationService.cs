using System.Linq.Expressions;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Validations;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.Application.Contracts.Services
{
    public interface IReservationService
    {
        Task<OperationResult> GetAllReservationsAsync(Expression<Func<Reservation, bool>> filter);
        Task<OperationResult> GetReservationByIdAsync(int id);
        Task<OperationResult> CreateReservationAsync(CreateReservationRequestDto request);
        Task<OperationResult> UpdateReservationAsync(UpdateReservationRequestDto request);
        Task<OperationResult> DeleteReservationAsync(int id);
        

        //Task<OperationResult> ConfirmReservationAsync(ConfirmReservationRequestDto request);
        //Task<OperationResult> ExpireConfirmedReservationsAsync();
    }
}
