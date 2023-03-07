using System.Composition;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

public class BasketViewModelTests
{
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new List<BasketItemViewModel>
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
            }, 300 },
            new object[] { new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 10,
                    UnitPrice = 40m
                }
            }, 0 },            
            new object[] { new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 1,
                    UnitPrice = 500m
                }
            }, 0 },
            new object[] { new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 1,
                    UnitPrice = 50m
                }
            }, 300 },
        };
    private BasketViewModel? _basketViewModel;

    [Theory]
    [MemberData(nameof(Data))]
    public void DeliveryFee(List<BasketItemViewModel> basketItemViewModels, decimal expected)
    {
        // Arrange
        _basketViewModel = new BasketViewModel
        {
            Id = 1,
            BuyerId = "buyer1",
            Items = basketItemViewModels
        };
        
        // Act
        _basketViewModel.Total();

        // Assert
        Assert.Equal(expected, _basketViewModel.Delivery);
    }
}
