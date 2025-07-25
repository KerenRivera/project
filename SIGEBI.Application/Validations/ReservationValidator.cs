using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.core;
using SIGEBI.Domain.Entities.library;

namespace SIGEBI.Application.Validations
{
    public class ReservationValidator
    {
        public static OperationResult ValidateReservation(Reservation entity)
        {
            if (entity == null)
            {
                return OperationResult.Failure("Reservation entity cannot be null.");
            }       

            if (entity.UserId <= 0)
            {
                return OperationResult.Failure("UserId is required and must be greater than 0.");
            }

            if (entity.ReservationDate.Date < DateTime.Today)
            {
                return OperationResult.Failure("Reservation date cannot be in the past.");
            }

            if (entity.ExpirationDate <= entity.ReservationDate)
            {
                return OperationResult.Failure("Expiration date must be after the reservation date.");
            }

            if (entity.StatusId <= 0)
            {
                return OperationResult.Failure("StatusId is required and must be greater than 0.");
            }

            return OperationResult.Success("Reservation validated successfully..");
        }

        public static OperationResult ValidateReservationStatus(ReservationStatus status)
        {
            if (status == null)
            {
                return OperationResult.Failure("Reservation status cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(status.StatusName))
            {
                return OperationResult.Failure("Status name is required and cannot be empty.");
            }
            if (status.StatusName.Length > 50)
            {
                return OperationResult.Failure("Status name cannot exceed 50 characters.");
            }
            return OperationResult.Success("Status validation passed.");
        }

        public static OperationResult ValidateReservationHistory(ReservationHistory history)
        {
            if (history == null)
            {
                return OperationResult.Failure("Reservation history cannot be null.");
            }
            if (history.ReservationId <= 0)
            {
                return OperationResult.Failure("ReservationId is required and must be greater than 0.");
            }
            if (history.UserId <= 0)
            {
                return OperationResult.Failure("UserId is required and must be greater than 0.");
            }
            if (history.ReservationDate == default)
            {
                return OperationResult.Failure("ActionDate is required and must be a valid date.");
            }
            return OperationResult.Success("Reservation history validation passed.");
        }

        public static OperationResult ValidateBookAvailability(Books book)
        {
            if (book == null)
                return OperationResult.Failure("Book cannot be null. Ensure the book exists in the system.");

            if (book.AvailableCopies <= 0)
                return OperationResult.Failure("The book is not available for reservation. No copies are available.");

            return OperationResult.Success("Book availability validated successfully.");
        }
        public static OperationResult ValidateReservationId(int id)
        {
            if (id <= 0)
            {
                return OperationResult.Failure("Reservation ID must be greater than 0.");
            }

            return OperationResult.Success("ID validation passed.");
        }
    }
}

