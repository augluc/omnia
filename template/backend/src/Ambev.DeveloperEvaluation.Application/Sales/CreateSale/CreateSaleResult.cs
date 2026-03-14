using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Dados devolvidos após a criação bem sucedida da Venda.
    /// </summary>
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public long TotalSaleAmount { get; set; }
    }
}
