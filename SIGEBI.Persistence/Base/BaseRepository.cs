using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Contracts;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Persistence.Base 
{
    // this class maneja operaciones cruds de forma reutilizable que hereda de la interfaz IBaseRepository from Application
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {
        protected readonly SIGEBIContext _context;

        public BaseRepository(SIGEBIContext context)
        {
            _context = context;
        }
        public async Task<OperationResult> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                    return OperationResult.Failure("Entity not found.");

                return OperationResult.Success("Entity retrieved successfully.", entity);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred retrieving the entity: " + ex.Message);
            }

        }

        public async Task<OperationResult> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var data = await _context.Set<T>().Where(filter).ToListAsync();
                return OperationResult.Success("Entities retrieved successfully.", data);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred retrieving entities: " + ex.Message);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _context.Set<T>().AnyAsync(filter);
            }
            catch
            {
                return false;
            }
        }

        public async Task<OperationResult> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success("Entity added successfully.", entity);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred adding the entity: " + ex.Message);
            }
        }

        public async Task<OperationResult> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success("Entity updated successfully.", entity);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred updating the entity: " + ex.Message);
            }
        }

        public async Task<OperationResult> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return OperationResult.Success("Entity deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred deleting the entity: " + ex.Message);
            }
        }
    }
}
