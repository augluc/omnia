using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementação do repositório para a entidade Sale.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Sale>> GetPagedAsync(int page, int size, string order, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales.AsQueryable();

            if (!string.IsNullOrWhiteSpace(order))
            {
                if (order.Contains("desc", StringComparison.OrdinalIgnoreCase))
                    query = query.OrderByDescending(s => s.SaleDate);
                else
                    query = query.OrderBy(s => s.SaleDate);
            }
            else
            {
                query = query.OrderByDescending(s => s.SaleDate);
            }

            return await query
                .Include(s => s.Products)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetPagedAsync(int page, int size, string order, SaleFilters? filters = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales.AsQueryable();

            query = ApplyFilters(query, filters);

            if (!string.IsNullOrWhiteSpace(order))
            {
                if (order.Contains("desc", StringComparison.OrdinalIgnoreCase))
                    query = query.OrderByDescending(s => s.SaleDate);
                else
                    query = query.OrderBy(s => s.SaleDate);
            }
            else
            {
                query = query.OrderByDescending(s => s.SaleDate);
            }

            return await query
                .Include(s => s.Products)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(SaleFilters? filters = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales.AsQueryable();

            query = ApplyFilters(query, filters);

            return await query.CountAsync(cancellationToken);
        }

        private IQueryable<Sale> ApplyFilters(IQueryable<Sale> query, SaleFilters? filters)
        {
            if (filters == null) return query;

            if (!string.IsNullOrWhiteSpace(filters.SaleNumber))
                query = ApplyStringFilter(query, s => s.SaleNumber, filters.SaleNumber);

            if (!string.IsNullOrWhiteSpace(filters.CustomerName))
                query = ApplyStringFilter(query, s => s.CustomerName, filters.CustomerName);

            if (!string.IsNullOrWhiteSpace(filters.BranchName))
                query = ApplyStringFilter(query, s => s.BranchName, filters.BranchName);

            if (filters.MinTotalAmount.HasValue)
                query = query.Where(s => s.TotalSaleAmount >= filters.MinTotalAmount.Value);

            if (filters.MaxTotalAmount.HasValue)
                query = query.Where(s => s.TotalSaleAmount <= filters.MaxTotalAmount.Value);

            if (filters.MinSaleDate.HasValue)
                query = query.Where(s => s.SaleDate >= filters.MinSaleDate.Value);

            if (filters.MaxSaleDate.HasValue)
                query = query.Where(s => s.SaleDate <= filters.MaxSaleDate.Value);

            return query;
        }

        private IQueryable<Sale> ApplyStringFilter(IQueryable<Sale> query, System.Linq.Expressions.Expression<Func<Sale, string>> propertySelector, string filterValue)
        {
            var propertyName = ((System.Linq.Expressions.MemberExpression)propertySelector.Body).Member.Name;

            if (filterValue.StartsWith("*") && filterValue.EndsWith("*"))
            {
                var val = filterValue.Trim('*');
                return query.Where(s => EF.Property<string>(s, propertyName).Contains(val));
            }
            if (filterValue.StartsWith("*"))
            {
                var val = filterValue.TrimStart('*');
                return query.Where(s => EF.Property<string>(s, propertyName).EndsWith(val));
            }
            if (filterValue.EndsWith("*"))
            {
                var val = filterValue.TrimEnd('*');
                return query.Where(s => EF.Property<string>(s, propertyName).StartsWith(val));
            }

            return query.Where(s => EF.Property<string>(s, propertyName) == filterValue);
        }
    }
}
