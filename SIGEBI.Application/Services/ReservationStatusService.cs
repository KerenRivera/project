using System.Linq.Expressions;
using SIGEBI.Application.Contracts.Repositories;
using SIGEBI.Application.Contracts.Repositories.Reservations;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Validations;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;


namespace SIGEBI.Application.Services
{
    public class ReservationStatusService : IReservationStatusService
    {
        private readonly IReservationStatusesRepository _reservationStatusesRepository;

        public ReservationStatusService(IReservationStatusesRepository reservationStatusesRepository)
        {
            _reservationStatusesRepository = reservationStatusesRepository;
        }

        public async Task<OperationResult> GetAllStatusesAsync(Expression<Func<ReservationStatus, bool>> filter)
        {
            var result = await _reservationStatusesRepository.GetAllAsync(x => true);

            if (!result.IsSuccess)
            {
                return OperationResult.Failure(result.Message);
            }

            return OperationResult.Success("Reservation statuses retrieved successfully.", result.Data);
        }

        public async Task<OperationResult> GetStatusByIdAsync(int id)
        {
            var validationResult = ReservationValidator.ValidateReservationId(id);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var result = await _reservationStatusesRepository.GetByIdAsync(id);

            if (!result.IsSuccess)
                return OperationResult.Failure(result.Message);

            return OperationResult.Success("Reservation status retrieved successfully.", result.Data);          
        }
    }
}
