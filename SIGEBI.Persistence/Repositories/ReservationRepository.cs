using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Contracts;
using SIGEBI.Application.Contracts.Repositories.Reservations;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Validations;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Constants;
using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Interfaces;
using IReservationRepository = SIGEBI.Persistence.Interfaces.IReservationRepository;


namespace SIGEBI.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly SIGEBIContext _context;
        private readonly IAppLogger<ReservationRepository> _logger;

        public ReservationRepository(SIGEBIContext context, IAppLogger<ReservationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<string> GetStatusNameById(int statusId)
        {
            try
            {
                if (statusId == 0)
                    return "Estado desconocido";

                var statusName = await _context.ReservationStatuses
                    .Where(s => s.Id == statusId)
                    .Select(s => s.StatusName)
                    .FirstOrDefaultAsync();

                return statusName ?? "Estado desconocido";
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error getting status name for StatusId: {StatusId}", statusId);
                return "Estado desconocido";
            }
        }

        private async Task<Reservation?> FindReservationAsync(int id)
        {
            _logger.LogInformation("Searching for reservation with Id: {Id}", id);
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                _logger.LogWarning("Reservation with Id {Id} not found.", id);
            }
            return reservation;
        }
        public async Task<OperationResult> AddAsync(Reservation entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                _logger.LogInformation("Adding reservation entity: {@Reservation}", entity);

               if (entity == null)
               {
                    _logger.LogError("Attemted to add a null Reservation entity.");
                    return OperationResult.Failure("Reservation cannot be null");        
               }
                //EF               

                _logger.LogInformation("Adding reservation entity: {@Reservation}", entity);

                await _context.Reservations.AddAsync(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Reservation entity added successfully: {@Entity}", entity);
                operationResult = OperationResult.Success("Reservation entity added succesfully", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding reservation entity.");
                operationResult = OperationResult.Failure("An Error occurred adding the reservation entity: " + ex.Message);
            }
            return operationResult;
        }
        public async Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> filter)
        {
            return await _context.Reservations.AnyAsync(filter);
        }
        public async Task<OperationResult> GetAllAsync(Expression<Func<Reservation, bool>> filter = null)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving reservations with filter: {Filter}.", filter?.ToString() ?? "No filter");

                var query = _context.Reservations
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .AsQueryable();

                if (filter != null)
                    query = query.Where(filter);

                var reservations = await query.ToListAsync();

                if (reservations == null || !reservations.Any())
                {
                    _logger.LogInformation("No reservations found matching the criteria.");
                    return OperationResult.Success("No reservations found.", new List<ReservationDto>());
                }

                var dtoList = new List<ReservationDto>();

                foreach (var r in reservations)
                {
                    string statusName = r.StatusId != 0
                        ? await GetStatusNameById(r.StatusId)
                        : "Unknown Status";

                    dtoList.Add(new ReservationDto
                    {
                        ReservationId = r.Id,
                        UserName = r.User?.FullName ?? "Usuario desconocido",
                        BookTitle = r.Book?.Title ?? "Titulo desconocido",
                        ReservationDate = r.ReservationDate,
                        ExpirationDate = r.ExpirationDate,
                        StatusName = statusName,
                    });
                }

                _logger.LogInformation("Retrieved {Count} reservations successfully.", dtoList.Count);
                return OperationResult.Success("Reservations retrieved successfully.", dtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reservations.");
                return OperationResult.Failure("An error occurred retrieving reservations: " + ex.Message);
            }
        }
        public async Task<OperationResult> GetByIdAsync(int id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving reservation entity by Id: {Id}", id);
                var reservation = await _context.Reservations.FindAsync(id);

                if (reservation == null || reservation.IsDeleted)
                {
                    _logger.LogWarning("Reservation with Id {Id} not found or is deleted.", id);
                    return OperationResult.Failure("Reservation not found.");
                }

                operationResult = OperationResult.Success("Reservation retrieved successfully.", reservation);
                _logger.LogInformation("Reservation retrieved successfully: {@Data}", reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reservation by Id: {Id}", id);
                operationResult = OperationResult.Failure("An error occurred retrieving the reservation: " + ex.Message);
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(Reservation entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Updating reservation entity: {@Entity}", entity);

                if (entity == null)
                {
                    _logger.LogError("Attempted to update a null Reservation entity.");
                    return OperationResult.Failure("Reservation entity cannot be null.");
                }

                Reservation? existingReservation = await _context.Reservations.FindAsync(entity.Id);
                if (existingReservation == null)
                {
                    _logger.LogWarning("Reservation with Id {Id} not found for update.", entity.Id);
                    return OperationResult.Failure("Reservation not found for update.");
                }

                existingReservation.BookId = entity.BookId;
                existingReservation.UserId = entity.UserId;
                existingReservation.StatusId = entity.StatusId;
                existingReservation.ReservationDate = entity.ReservationDate;

                existingReservation.UpdatedAt = entity.UpdatedAt;


                if (existingReservation.GetType().GetProperty("UpdatedAt") != null)
                {
                    existingReservation.GetType().GetProperty("UpdatedAt")?.SetValue(existingReservation, DateTime.UtcNow);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Reservation updated successfully: {@Entity}", existingReservation);
                return OperationResult.Success("Reservation entity updated successfully.", existingReservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating reservation entity: {EntityId}", entity?.Id);
                return OperationResult.Failure("An error occurred updating the reservation entity: " + ex.Message);
            }
        }

        public async Task<OperationResult> DeleteAsync(Reservation entity)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Attempted to delete a null Reservation entity.");
                    return OperationResult.Failure("Reservation cannot be null");
                }
                _logger.LogInformation("Deleting reservation entity: {@Entity}", entity);

                Reservation? existingEntity = await _context.Reservations.FindAsync(entity.Id);

                if (existingEntity is null || existingEntity.IsDeleted)
                {
                    _logger.LogWarning("Reservation with Id {Id} not found for deletion.", entity.Id);
                    return OperationResult.Failure("Reservation not found for deletion.");
                }
                existingEntity.IsDeleted = true;
                existingEntity.DeletedBy = Environment.UserName;
                _context.Reservations.Update(existingEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting reservation entity.");
                operationResult = OperationResult.Failure("An error occurred deleting the reservation entity: " + ex.Message);
              
            }
            return operationResult;
        }
        
    }
}
