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

        public async Task<OperationResult> GetAllReservationsAsync(Expression<Func<Reservation, bool>> filter = null) //terminar de modificar
        {
            var result = await _reservationRepository.GetAllAsync(filter);

            if (!result.IsSuccess)
            {
                return result;
            }

            if (result.Data == null)
            {
                return OperationResult.Failure("No reservations found.");
            }

            var reservations = (result.Data as IEnumerable<Reservation>) ?? new List<Reservation>();

            var dtoList = new List<ReservationDto>();

            foreach (var r in reservations)
            {
                var statusName = r.StatusId != 0 ? await _reservationStatusesRepository.GetStatusNameByIdAsync(r.StatusId) ?? "Estado desconocido" : "Estado desconocido";
                dtoList.Add(new ReservationDto
                {
                    ReservationId = r.Id,
                    UserName = r.User?.FullName ?? "Usuario desconocido",
                    BookTitle = r.Book?.Title ?? "Titulo desconocido",
                    ReservationDate = r.ReservationDate,
                    ExpirationDate = r.ExpirationDate,
                    StatusName = statusName ?? "Estado desconocido",
                });
            }

            return OperationResult.Success("Reservations retrieved successfully.", dtoList);
        }

        public Task<OperationResult> GetReservationByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Reservation ID must be greater than zero.");
            }

            return _reservationRepository.GetByIdAsync(id);
        } //funciona

        public async Task<OperationResult> UpdateReservationAsync(UpdateReservationRequestDto request) //chequear si funciona pero antes chequear el metodo en el repo
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

            // buscamos la reserva existente

            var Result = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (!Result.IsSuccess || Result.Data is not Reservation existingReservation)
            {
                return OperationResult.Failure("Reservation not found.");
            }

            //verificamos que el libro este disponible (en este caso siempre lo estara)

            bool isAvailable = await _bookService.IsBookAvailableAsync(request.BookId); // verificar

            if (isAvailable)
            {
                return OperationResult.Failure("The book is not available for reservation.");
            }

            var reservation = (Reservation)Result.Data;

            reservation.BookId = request.BookId;

            existingReservation.UpdatedAt = DateTime.UtcNow;

            var validationResult = ReservationValidator.ValidateReservation(reservation);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }
            return await _reservationRepository.UpdateAsync(reservation);

            var status = await _reservationStatusesRepository.GetStatusNameByIdAsync(existingReservation.StatusId);

            var response = new ReservationUpdateResponseDto
            {
                ReservationId = existingReservation.Id,
                BookId = existingReservation.BookId,
                StatusName = status,
                UpdatedAt = (DateTime)existingReservation.UpdatedAt
            };
            return OperationResult.Success("Reservation updated successfully.", response);
        }
    }
}

