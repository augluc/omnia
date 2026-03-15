using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesRequest
    {
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;

        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;

        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;

        [FromQuery(Name = "saleNumber")]
        public string? SaleNumber { get; set; }

        [FromQuery(Name = "customerName")]
        public string? CustomerName { get; set; }

        [FromQuery(Name = "branchName")]
        public string? BranchName { get; set; }

        [FromQuery(Name = "_minTotalAmount")]
        public long? MinTotalAmount { get; set; }

        [FromQuery(Name = "_maxTotalAmount")]
        public long? MaxTotalAmount { get; set; }

        [FromQuery(Name = "_minSaleDate")]
        public DateTime? MinSaleDate { get; set; }

        [FromQuery(Name = "_maxSaleDate")]
        public DateTime? MaxSaleDate { get; set; }
    }
}
