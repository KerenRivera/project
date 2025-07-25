using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.WebApi.Services
{
    public class ReservationStatusApiService
    {
        private readonly IReservationStatusService _reservationStatusService;

        public ReservationStatusApiService(IReservationStatusService reservationStatusService)
        {
            _reservationStatusService = reservationStatusService;
        }

        private object MapToDto(ReservationStatus status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status), "Reservation status cannot be null.");

            return new ReservationStatusDto
            {
                StatusId = status.Id,
                StatusName = status.StatusName
            };      
        }

        public async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var result = await _reservationStatusService.GetAllStatusesAsync(x => true);

                if (!result.IsSuccess || result.Data == null)
                    throw new InvalidOperationException(result.Message);

                var status = result.Data as List<ReservationStatus>;
                if (status == null)
                    throw new InvalidOperationException("Failed to cast result data to List<ReservationStatus>.");

                var dtos = status.Select(MapToDto).ToList();

                return OperationResult.Success("Statuses retrieved successfully", dtos);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error retrieving reservation statuses: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new InvalidOperationException("Invalid reservation status ID.");

                var result = await _reservationStatusService.GetStatusByIdAsync(id);

                if (!result.IsSuccess || result.Data == null)
                    return OperationResult.Failure("Reservation status not found.");

                var status = result.Data as ReservationStatus;

                if (status == null) 
                    throw new InvalidOperationException("Failed to cast result data to ReservationStatus.");

                var dto = MapToDto(status);

                return OperationResult.Success("Reservation status retrieved successfully.", dto);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error retrieving reservation status: {ex.Message}");
            }
        }
    }
}
   


