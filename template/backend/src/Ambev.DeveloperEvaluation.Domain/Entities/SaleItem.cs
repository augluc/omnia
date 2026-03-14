using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa um item individual dentro de uma venda.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public long UnitPrice { get; private set; }
        public long Discount { get; private set; }
        public long TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        protected SaleItem() { }

        public SaleItem(Guid productId, string productName, int quantity, long unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;

            SetQuantity(quantity);
        }

        /// <summary>
        /// Define a quantidade e aplica as regras de desconto correspondentes.
        /// </summary>
        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");

            Quantity = quantity;
            CalculateDiscountsAndTotal();
        }

        /// <summary>
        /// Cancela o item da venda.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Lógica de negócio (Domínio Rico): Calcula os descontos de acordo com a quantidade.
        /// </summary>
        private void CalculateDiscountsAndTotal()
        {
            long grossAmount = UnitPrice * Quantity;
            decimal discountPercentage = 0m;

            if (Quantity >= 4 && Quantity < 10)
            {
                discountPercentage = 0.10m;
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                discountPercentage = 0.20m;
            }

            Discount = (long)(grossAmount * discountPercentage);
            TotalAmount = grossAmount - Discount;
        }
    }
}
