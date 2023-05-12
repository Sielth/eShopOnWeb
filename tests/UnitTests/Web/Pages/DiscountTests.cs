using Microsoft.eShopWeb.Infrastructure.Identity;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => TDD Example

public class DiscountTests
{
    [Fact]
    public void CalculatePointDiscount()
    {
        // Arrange
        var user = new MemberUser { Email = "test", MemberPoints = 200 };
        // Act
        
        // Step 1: Find logic to calculate discount
        decimal total = 200;
        decimal points = user.MemberPoints;
        decimal procentDiscount = points / 10;
        decimal multiplier = (100 - procentDiscount) / 100;
        decimal actual = total * multiplier;
        decimal expected = 160;
        
        // Step 2: Write and Test method
        // Step 3: See method fail
        // Step 4: Move logic discount in the method
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
