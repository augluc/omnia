namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public long TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
