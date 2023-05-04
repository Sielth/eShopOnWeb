using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

public class DiscountTests
{
    [Fact]
    public void CalculatePointDiscount()
    {
        // Arrange
        var user = new MemberUser { Email = "test", MemberPoints = 200 };
        // Act
        // Find logic to calculate discount
        decimal total = 200;
        decimal points = user.MemberPoints;
        decimal procentDiscount = points / 10;
        decimal multiplier = (100 - procentDiscount) / 100;
        decimal actual = total * multiplier;
        decimal expected = 160;
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
