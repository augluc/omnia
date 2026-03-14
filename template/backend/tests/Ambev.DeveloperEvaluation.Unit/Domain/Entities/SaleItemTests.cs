using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Testes unitários para as regras de negócio do item da venda.
/// </summary>
public class SaleItemTests
{
    [Fact]
    public void Given_QuantityBelowFour_When_SetQuantity_Then_NoDiscountApplied()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product A", 1, 1000);

        // Act (Ação)
        item.SetQuantity(3);

        // Assert
        Assert.Equal(0, item.Discount);
        Assert.Equal(3000, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityBetweenFourAndNine_When_SetQuantity_Then_TenPercentDiscountApplied()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product B", 1, 1000);

        // Act
        item.SetQuantity(5);

        // Assert
        Assert.Equal(500, item.Discount);
        Assert.Equal(4500, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityBetweenTenAndTwenty_When_SetQuantity_Then_TwentyPercentDiscountApplied()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product C", 1, 1000);

        // Act
        item.SetQuantity(15); 

        // Assert
        Assert.Equal(3000, item.Discount);
        Assert.Equal(12000, item.TotalAmount);
    }

    [Fact]
    public void Given_QuantityAboveTwenty_When_SetQuantity_Then_ThrowsDomainException()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product D", 1, 1000);

        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => item.SetQuantity(21));
        Assert.Equal("Cannot sell more than 20 identical items.", exception.Message);
    }

    [Fact]
    public void Given_QuantityZeroOrNegative_When_SetQuantity_Then_ThrowsDomainException()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product E", 1, 1000);

        // Act & Assert
        Assert.Throws<DomainException>(() => item.SetQuantity(0));
        Assert.Throws<DomainException>(() => item.SetQuantity(-5));
    }
}