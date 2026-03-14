using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa uma Venda (Aggregate Root).
    /// </summary>
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; } = string.Empty;
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; } = string.Empty;

        public long TotalSaleAmount { get; private set; }
        public bool IsCancelled { get; private set; }

        private readonly List<SaleItem> _products = new();
        public IReadOnlyCollection<SaleItem> Products => _products.AsReadOnly();

        protected Sale() { }

        public Sale(string saleNumber, DateTime saleDate, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            IsCancelled = false;
        }

        /// <summary>
        /// Adiciona um item à venda e recalcula o total.
        /// </summary>
        public void AddItem(SaleItem item)
        {
            _products.Add(item);
            RecalculateTotal();
        }

        /// <summary>
        /// Cancela a venda inteira (Soft Delete).
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
            foreach (var item in _products)
            {
                item.Cancel();
            }
        }

        /// <summary>
        /// Cancela um item específico da venda.
        /// </summary>
        public void CancelItem(Guid saleItemId)
        {
            var item = _products.FirstOrDefault(i => i.Id == saleItemId);
            if (item != null)
            {
                item.Cancel();
                RecalculateTotal();
            }
        }

        /// <summary>
        /// Recalcula o valor total da venda somando os itens não cancelados.
        /// </summary>
        private void RecalculateTotal()
        {
            TotalSaleAmount = _products
                .Where(p => !p.IsCancelled)
                .Sum(p => p.TotalAmount);
        }
    }
}
