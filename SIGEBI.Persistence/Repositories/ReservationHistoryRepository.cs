using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.DTOs;
using SIGEBI.Application.Contracts;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Persistence.Interfaces;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Repositories
{
    public class ReservationHistoryRepository : IReservationHistoryRepository
    {
        private readonly SIGEBIContext _context;
        private readonly IAppLogger<ReservationHistoryRepository> _logger;

        public ReservationHistoryRepository(SIGEBIContext context, IAppLogger<ReservationHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> ExistsAsync(Expression<Func<ReservationHistory, bool>> filter)
        {
            return await _context.ReservationHistory.AnyAsync(filter);
        }

        public async Task<OperationResult> GetAllAsync()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving all reservation history.");

                var historyList = await _context.ReservationHistory
                    //.OrderByDescending(h => h.ReservationDate)
                    .ToListAsync();

                _logger.LogInformation("Reservation history records retrieved: {@Data}", historyList);

                operationResult = OperationResult.Success("Reservation history records retrived succesfully.", historyList);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all reservation history records.");
                operationResult = OperationResult.Failure("An error occurred retrieving reservation history records: " + ex.Message);
            }
            return operationResult;
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving reservation history by HistoryId: {HistoryId}", id);

                var entity = await _context.ReservationHistory.FindAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning("Reservation history record with Id {Id} not found.", id);
                    return OperationResult.Failure("Reservation history record not found");
                }
                

                _logger.LogInformation("Reservation history record retrieved: {@Entity}", entity);

                operationResult = OperationResult.Success("Reservation history record retrieved successfully.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reservation history by Id.");
                operationResult = OperationResult.Failure("An error occurred retrieving the reservation history record: " + ex.Message);
            }
            return operationResult;
        }
    }
}
