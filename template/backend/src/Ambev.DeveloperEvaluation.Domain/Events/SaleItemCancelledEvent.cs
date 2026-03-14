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
