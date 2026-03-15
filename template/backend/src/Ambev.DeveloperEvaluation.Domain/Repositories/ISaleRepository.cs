using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Contrato de repositório para a entidade Sale.
    /// </summary>
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Sale>> GetPagedAsync(int page, int size, string order, SaleFilters? filters = null, CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(SaleFilters? filters = null, CancellationToken cancellationToken = default);
    }

    public class SaleFilters
    {
        public string? SaleNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public long? MinTotalAmount { get; set; }
        public long? MaxTotalAmount { get; set; }
        public DateTime? MinSaleDate { get; set; }
        public DateTime? MaxSaleDate { get; set; }
    }
}
