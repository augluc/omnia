using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelledEvent
    {
        public Guid SaleId { get; }
        public Guid SaleItemId { get; }

        public SaleItemCancelledEvent(Guid saleId, Guid saleItemId)
        {
            SaleId = saleId;
            SaleItemId = saleItemId;
        }
    }
}
