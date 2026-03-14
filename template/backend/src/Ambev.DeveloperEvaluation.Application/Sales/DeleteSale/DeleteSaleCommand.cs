using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command para apagar/cancelar uma Venda.
    /// </summary>
    public record DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// O identificador único da venda a ser apagada/cancelada.
        /// </summary>
        public Guid Id { get; }

        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
