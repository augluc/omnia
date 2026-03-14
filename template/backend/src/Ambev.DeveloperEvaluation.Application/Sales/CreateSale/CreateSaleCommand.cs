using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Comando para criar uma nova venda.
    /// Implementa IRequest para indicar que este comando vai devolver um CreateSaleResult.
    /// </summary>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<CreateSaleItemCommand> Items { get; set; } = new();
    }

    /// <summary>
    /// DTO para os itens da venda dentro do comando de criação.
    /// </summary>
    public class CreateSaleItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public long UnitPrice { get; set; }
    }
}
