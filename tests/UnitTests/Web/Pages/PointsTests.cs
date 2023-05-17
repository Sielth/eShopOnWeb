using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => MemberData 

public class PointsTests
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
            }, 10 },
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
            }, 40 },            
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
            }, 50 },
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
            }, 0 },
        };
    
    private BasketViewModel? _basketViewModel;
    
    [Theory]
    [MemberData(nameof(Data))]
    public void MemberUserGetsPointsDependingOnTotal(List<BasketItemViewModel> basketItemViewModels, int expected)
    {
        // Arrange
        _basketViewModel = new BasketViewModel
        {
            Id = 1,
            BuyerId = "buyer1",
            Items = basketItemViewModels
        };

        // Act
        var points = _basketViewModel.AddPoints();

        // Assert
        Assert.Equal(expected, points);
    }
}
