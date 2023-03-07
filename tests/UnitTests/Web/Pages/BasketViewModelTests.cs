using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

public class BasketViewModelTests
{
    private BasketViewModel? _basketViewModel;

    [Fact]
    public void DeliveryFee()
    {
        // Arrange
        _basketViewModel = new BasketViewModel
        {
            Id = 1,
            BuyerId = "buyer1",
            Items = new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 10,
                    UnitPrice = 10m
                }
            }
        };
        
        var expectedDelivery = 300m;

        // Act
        _basketViewModel.Total();

        // Assert
        Assert.Equal(expectedDelivery, _basketViewModel.Delivery);
    }
}
