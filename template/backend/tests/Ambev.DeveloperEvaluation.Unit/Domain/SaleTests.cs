using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Testes unitários para o Aggregate Root Sale.
/// </summary>
public class SaleTests
{
    private Sale CreateBaseSale()
    {
        return new Sale("SALE-001", DateTime.UtcNow, Guid.NewGuid(), "Customer", Guid.NewGuid(), "Branch");
    }

    [Fact]
    public void Given_NewItems_When_AddedToSale_Then_TotalSaleAmountIsRecalculated()
    {
        // Arrange
        var sale = CreateBaseSale();
        var item1 = new SaleItem(Guid.NewGuid(), "Product A", 3, 1000);
        var item2 = new SaleItem(Guid.NewGuid(), "Product B", 5, 1000);

        // Act
        sale.AddItem(item1);
        sale.AddItem(item2);

        // Assert
        Assert.Equal(2, sale.Products.Count);
        Assert.Equal(7500, sale.TotalSaleAmount);
    }

    [Fact]
    public void Given_ExistingItem_When_Cancelled_Then_TotalSaleAmountIsRecalculated()
    {
        // Arrange
        var sale = CreateBaseSale();
        var item = new SaleItem(Guid.NewGuid(), "Product A", 10, 1000);
        sale.AddItem(item);

        Assert.Equal(8000, sale.TotalSaleAmount);

        // Act
        sale.CancelItem(item.Id);

        // Assert
        Assert.True(item.IsCancelled);
        Assert.Equal(0, sale.TotalSaleAmount);
    }

    [Fact]
    public void Given_ActiveSale_When_Cancelled_Then_AllItemsAreCancelled()
    {
        // Arrange
        var sale = CreateBaseSale();
        var item1 = new SaleItem(Guid.NewGuid(), "Product A", 2, 1000);
        var item2 = new SaleItem(Guid.NewGuid(), "Product B", 2, 1000);
        sale.AddItem(item1);
        sale.AddItem(item2);

        // Act
        sale.Cancel();

        // Assert
        Assert.True(sale.IsCancelled);
        Assert.All(sale.Products, p => Assert.True(p.IsCancelled));
    }
}