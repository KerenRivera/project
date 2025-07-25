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
    public class ReservationHistoryService : IReservationHistoryService
    {
        private readonly IReservationHistoryRepository _reservationHistoryRepository;
        public ReservationHistoryService(IReservationHistoryRepository reservationHistoryRepository)
        {
            _reservationHistoryRepository = reservationHistoryRepository;
        }
        public async Task<OperationResult> GetAllHistoriesAsync(Expression<Func<Reservation, bool>> filter)
        {
            var result = await _reservationHistoryRepository.GetAllAsync();

            if (!result.IsSuccess)
            {
                return OperationResult.Failure(result.Message);
            }
            return OperationResult.Success("Reservation histories retrieved successfully.", result.Data);

        }
        public async Task<OperationResult> GetHistoryByIdAsync(int id)
        {
            var validationResult = ReservationValidator.ValidateReservationId(id);
            if (!validationResult.IsSuccess)
                return validationResult;
            
            var result = await _reservationHistoryRepository.GetByIdAsync(id);

            if (!result.IsSuccess) 
            {
                return OperationResult.Failure(result.Message);
            }

            return OperationResult.Success("Reservation history retrieved successfully.", result.Data);
        }
    }
}
