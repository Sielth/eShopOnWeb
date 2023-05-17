using BlazorAdmin.Pages.CatalogItemPage;
using FluentAssertions;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => TDD Example

public class DiscountTests
{
    private readonly BasketViewModel _sut = new BasketViewModel();

    [Fact]
    public void CalculateDiscountBasedOnPoints()
    {
        // Arrange
        var user = new MemberUser { Email = "test", MemberPoints = 200 };

        // Act

        // Step 1: Find logic to calculate discount
        decimal total = 200;
        decimal points = user.MemberPoints;
        decimal percentDiscount = points / 10;
        decimal multiplier = (100 - percentDiscount) / 100;
        decimal actual = total * multiplier;
        decimal expected = 160;

        // Step 2: Write and Test method
        // Step 3: See method fail
        // Step 4: Move logic discount in the method

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(BasketItemGenerator))]
    public void CalculateDiscountOnPointsWithClassData(List<BasketItemViewModel> basketItemViewModels, decimal expected)
    {
        // Arrange
        _sut.Items = basketItemViewModels;
        var user = new MemberUser { Email = "test", MemberPoints = 200 };
        
        // Act
        // Step 2: Write and Test method
        var actual = _sut.GetTotalAfterDiscount(user.MemberPoints);
        // Step 3: See method fail
        // Step 4: Move logic discount in the method
        
        //Assert
        actual.Should().Be(expected);
    }
    
}
