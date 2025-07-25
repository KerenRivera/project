using System.Linq.Expressions;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.WebApi.Services
{
    public class ReservationHistoryApiService
    {
        private readonly IReservationHistoryService _reservationHistoryService;

        public ReservationHistoryApiService(IReservationHistoryService reservationHistoryService)
        {
            _reservationHistoryService = reservationHistoryService;
        }

        private object MapToDto(ReservationHistory history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history), "Reservation history cannot be null.");

            return new ReservationHistoryDto
            {
                HistoryId = history.ReservationHistoryId,
                ReservationId = history.ReservationId,
                BookTitle = history.Book?.Title ?? string.Empty, 
                UserName = history.User?.FullName ?? string.Empty,
                StatusName = history.ReservationStatus?.StatusName ?? string.Empty,
                ReservationDate = history.ReservationDate,
                ExpirationDate = history.ExpirationDate
            };
        }
        public async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var result = await _reservationHistoryService.GetAllHistoriesAsync(x => true);

                if (!result.IsSuccess)
                    return result;

                // Use null-coalescing operator to handle possible null value
                var histories = result.Data as List<ReservationHistory> ?? new List<ReservationHistory>();

                var dtoList = histories.Select(h => MapToDto(h)).ToList();

                return OperationResult.Success("Reservation histories retrieved successfully.", dtoList);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving reservation histories: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new InvalidOperationException("Invalid ID.");

                var result = await _reservationHistoryService.GetHistoryByIdAsync(id);

                if (!result.IsSuccess || result.Data is not ReservationHistory history)
                    throw new InvalidOperationException("Reservation history not found.");

                var dto = MapToDto(history);

                return OperationResult.Success("Reservation history retrieved successfully.", dto);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"An error occurred while retrieving reservation history: {ex.Message}");
            }
        }
    }
}
