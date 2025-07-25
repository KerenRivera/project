using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.api.Services 
{
    public class ReservationApiService
    {
        private readonly IReservationService _reservationService;

        public ReservationApiService(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<OperationResult> GetAllReservationsAsync(Expression<Func<Reservation, bool>> filter)
        {
            return await _reservationService.GetAllReservationsAsync(filter);
        }

        public async Task<OperationResult> GetReservationByIdAsync(int id)
        {
            return await _reservationService.GetReservationByIdAsync(id);
        }

        public async Task<OperationResult> CreateReservationAsync(CreateReservationRequestDto dto)
        {
            return await _reservationService.CreateReservationAsync(dto);
        }

        public async Task<OperationResult> UpdateReservationAsync(UpdateReservationRequestDto dto)
        {
            return await _reservationService.UpdateReservationAsync(dto);
        }

        public async Task<OperationResult> DeleteReservationAsync(int id)
        {
            return await _reservationService.DeleteReservationAsync(id);
        }

        //    private object MaptoDto(Reservation r)
        //    {
        //        if (r == null)
        //            throw new ArgumentNullException(nameof(r), "Reservation cannot be null.");

        //        // Map Reservation to DTO
        //        return new ReservationDto
        //        {
        //            ReservationId = r.Id,
        //            UserName = r.User?.FullName ?? string.Empty, 
        //            BookTitle = r.Book?.Title ?? string.Empty, 
        //            ReservationDate = r.ReservationDate,
        //            ExpirationDate = r.ExpirationDate ?? DateTime.MinValue, 
        //            StatusName = r.ReservationStatus?.StatusName ?? string.Empty 
        //        };
        //    }

        //    public async Task<OperationResult> CreateReservationAsync(CreateReservationRequestDto requestDto)
        //    {
        //        try
        //        {
        //            var result = await _reservationService.CreateReservationAsync(requestDto);

        //            if (!result.IsSuccess)
        //                return OperationResult.Failure(result.Message);

        //            if (result.Data is not Reservation reservation)
        //                return OperationResult.Failure("Invalid reservation data.");

        //            var dto = MaptoDto(reservation);
        //            return OperationResult.Success("Reservation created successfully.", dto);
        //        }
        //        catch (Exception ex)
        //        {
        //            return OperationResult.Failure($"An error occurred while creating the reservation: {ex.Message}");
        //        }
        //    }

        //    public async Task<OperationResult> UpdateReservationAsync(UpdateReservationRequestDto requestDto)
        //    {
        //        try
        //        {
        //            var result = await _reservationService.UpdateReservationAsync(requestDto);

        //            if (!result.IsSuccess)
        //                return OperationResult.Failure(result.Message);

        //            if (result.Data is not Reservation reservation)
        //                return OperationResult.Failure("Invalid reservation data.");

        //            var dto = MaptoDto(reservation);
        //            return OperationResult.Success("Reservation updated successfully.", dto);
        //        }
        //        catch (Exception ex)
        //        {
        //            return OperationResult.Failure($"An error occurred while updating the reservation: {ex.Message}");
        //        }
        //    }

        //    public async Task<OperationResult> GetAllReservationsAsync(Expression<Func<Reservation, bool>> filter)
        //    {
        //        try
        //        {
        //            var result = await _reservationService.GetAllReservationsAsync(filter);

        //            if (!result.IsSuccess)
        //                return OperationResult.Success("No reservations found.", new List<ReservationDto>());

        //            var reservations = ((List<Reservation>)result.Data ?? new List<Reservation>())
        //                .Where(r => r != null)
        //                .Select(r => MaptoDto(r))
        //                .ToList();

        //            return OperationResult.Success("Reservations retrieved successfully", reservations);
        //        }
        //        catch (Exception ex)
        //        {
        //            return OperationResult.Failure($"An error occurred while retrieving reservations: {ex.Message}");
        //        }
        //    }

        //    public async Task<OperationResult> GetReservationByIdAsync(int id)
        //    {
        //        try
        //        {
        //            var result = await _reservationService.GetReservationByIdAsync(id);

        //            if (!result.IsSuccess || result.Data is not Reservation reservation || reservation == null)
        //                return OperationResult.Failure("Reservation not found.");

        //            var dto = MaptoDto(reservation);
        //            return OperationResult.Success("Reservation retrieved successfully", dto);
        //        }
        //        catch (Exception ex)
        //        {
        //            return OperationResult.Failure($"An error occurred while retrieving the reservation: {ex.Message}");
        //        }
        //    }

        //    public async Task<OperationResult> DeleteReservationAsync(int id)
        //    {
        //        try
        //        {
        //            var result = await _reservationService.DeleteReservationAsync(id);

        //            if (!result.IsSuccess)
        //                return OperationResult.Failure(result.Message);

        //            return OperationResult.Success("Reservation deleted successfully.");
        //        }
        //        catch (Exception ex)
        //        {
        //            return OperationResult.Failure($"An error occurred while deleting the reservation: {ex.Message}");
        //        }
        //    }
    }
}

