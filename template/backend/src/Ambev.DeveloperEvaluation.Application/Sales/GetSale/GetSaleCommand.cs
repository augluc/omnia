using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command (Query) para recuperar uma venda pelo seu ID.
    /// </summary>
    public record GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// O identificador único da venda.
        /// </summary>
        public Guid Id { get; }

        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
