namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Contrato de saída após a criação de uma Venda com sucesso.
    /// </summary>
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public long TotalSaleAmount { get; set; }
    }
}
