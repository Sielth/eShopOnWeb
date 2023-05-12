using System.Composition;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => MemberData Tests

public class BasketViewModelTests
{
    private BasketViewModel? _basketViewModel;

    [Theory]
    [MemberData(nameof(GetBasketViewModel.Data), MemberType= typeof(GetBasketViewModel))]
    public void DeliveryFee(List<BasketItemViewModel> basketItemViewModels, decimal expected)
    {
        // Arrange
        _basketViewModel = new BasketViewModel
        {
            Id = 1,
            BuyerId = "Test buyerId",
            Items = basketItemViewModels
        };
        
        // Act
        var delivery = _basketViewModel.CalculateDelivery();

        // Assert
        Assert.Equal(expected, _basketViewModel.CalculateDelivery());
    }

    [Theory]
    [MemberData(nameof(GetBasketViewModel.Data), MemberType= typeof(GetBasketViewModel))]
    public void MemberUserGetsPointsDependingOnTotal(List<BasketItemViewModel> basketItemViewModels)
    {
        // Arrange
        _basketViewModel = new BasketViewModel
        {
            Id = 1,
            BuyerId = "Test buyerId",
            Items = basketItemViewModels
        };

        var expected = 105;
        var totalWithoutDelivery = _basketViewModel.CalculateItemsTotalPrice();

        // Act
        var points = _basketViewModel.AddPoints();
        // var points = Math.Floor(totalWithoutDelivery/10);

        // Assert
        Assert.Equal(expected, points);
    }
}
