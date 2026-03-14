namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public long TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public List<GetSaleItemResponse> Items { get; set; } = new();
    }

    public class GetSaleItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public long UnitPrice { get; set; }
        public long Discount { get; set; }
        public long TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
