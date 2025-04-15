using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new sale in the database
        /// </summary>
        /// <param name="sale">The sale to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale</returns>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }


        /// <summary>
        /// Retrieves a sale by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(s => s.Items).SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a sale by their sale number identifier
        /// </summary>
        /// <param name="saleNumber">The sale number identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetBySaleNumberAsync(long saleNumber, CancellationToken cancellationToken = default)
        {
           return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        /// <summary>
        /// Update a sale from the database
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Get all sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Sales
           .Include(s => s.Items)
           .AsQueryable();

            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes a sale from the database
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the user was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;
            
            foreach (var item in sale.Items)
            {
                _context.SaleItems.Remove(item);
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
