using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Resultado da operação GetSale com os detalhes da Venda.
    /// </summary>
    public class GetSaleResult
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
        public List<GetSaleItemResult> Items { get; set; } = new();
    }

    public class GetSaleItemResult
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
