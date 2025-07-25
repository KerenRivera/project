using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using SIGEBI.Application.Contracts;
using SIGEBI.Application.Contracts.Repositories;
using SIGEBI.Application.Contracts.Repositories.Reservations;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Validations;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;

namespace SIGEBI.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationStatusesRepository _reservationStatusesRepository;
        private readonly IBookService _bookService;

        public ReservationService(IReservationRepository reservationRepository, IReservationStatusesRepository reservationStatusesRepository)
        {
            _reservationRepository = reservationRepository;
            _reservationStatusesRepository = reservationStatusesRepository;
        }
        public async Task<OperationResult> CreateReservationAsync(CreateReservationRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Reservation request cannot be null.");
            }

            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                return OperationResult.Failure("Invalid reservation request.", validationResults);
            }

            var reservation = new Reservation //mapeo el dto a la entidad
            {
                BookId = request.BookId,
                UserId = request.UserId,
                CreatedBy = request.CreatedBy ?? "System",
                StatusId = request.StatusId,
                ReservationDate = request.ReservationDate,
                CreatedAt = request.CreatedAt,
                //UpdatedBy = request.UpdatedBy ?? string.Empty,
                //DeletedBy = request.DeletedBy ?? string.Empty
            };

            var validationResult = ReservationValidator.ValidateReservation(reservation);
            if (!validationResult.IsSuccess)
                return validationResult;

            return await _reservationRepository.AddAsync(reservation);
        } //funciona

        public async Task<OperationResult> DeleteReservationAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Reservation ID must be greater than zero.");

            var existing = await _reservationRepository.GetByIdAsync(id);
            if (existing?.Data is not Reservation reservationToDelete)
            {
                return OperationResult.Failure("Reservation not found.");
            }

            reservationToDelete.IsDeleted = true;
            reservationToDelete.DeletedAt = DateTime.UtcNow;
            reservationToDelete.DeletedBy = "System";

            return await _reservationRepository.UpdateAsync(reservationToDelete);
        } //funciona

        public async Task<OperationResult> GetAllReservationsAsync(Expression<Func<Reservation, bool>> filter = null)
        {
            if (_reservationRepository == null)
            {
                throw new InvalidOperationException("Reservation repository is not initialized.");
            }

            var result = await _reservationRepository.GetAllAsync(filter);
            return result;
        }
        

        public Task<OperationResult> GetReservationByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Reservation ID must be greater than zero.");
            }

            return _reservationRepository.GetByIdAsync(id);
        } //funciona

        public async Task<OperationResult> UpdateReservationAsync(UpdateReservationRequestDto request)
        {
            if (request == null)
            {
                return OperationResult.Failure("Reservation request cannot be null.");
            }

            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                return OperationResult.Failure($"Invalid reservation request: {errors}");
            }

            var result = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (!result.IsSuccess || result.Data is not Reservation existingReservation)
            {
                return OperationResult.Failure("Reservation not found.");
            }

            existingReservation.BookId = request.BookId;

            if (existingReservation.GetType().GetProperty("UpdatedAt") != null)
                existingReservation.GetType().GetProperty("UpdatedAt")?.SetValue(existingReservation, DateTime.UtcNow);
            

            var validationResult = ReservationValidator.ValidateReservation(existingReservation);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var updateResult = await _reservationRepository.UpdateAsync(existingReservation);
            if (!updateResult.IsSuccess)
            {
                return updateResult;
            }

            var updatedReservation = updateResult.Data as Reservation;
            if (updatedReservation == null)
            {
                return OperationResult.Failure("Error retrieving updated reservation.");
            }

            var status = await _reservationStatusesRepository.GetStatusNameByIdAsync(updatedReservation.StatusId);

            var response = new ReservationUpdateResponseDto
            {
                ReservationId = updatedReservation.Id,
                BookId = updatedReservation.BookId,
                StatusName = status ?? "Estado desconocido",
                UpdatedAt = updatedReservation.UpdatedAt ?? DateTime.UtcNow
            };

            return OperationResult.Success("Reservation updated successfully.", response);
        }
    }
}

