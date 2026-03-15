using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesQuery : IRequest<ListSalesResult>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = string.Empty;

        public string? SaleNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public long? MinTotalAmount { get; set; }
        public long? MaxTotalAmount { get; set; }
        public DateTime? MinSaleDate { get; set; }
        public DateTime? MaxSaleDate { get; set; }
    }
}
