using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Services;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Services;

// TODO: KEY: EXAM => ClassData + InlineData + Mocking

public class BasketViewModelServiceTests
{
    private readonly Mock<IRepository<Basket>> _basketRepository = new();
    private readonly Mock<IUriComposer> _uriComposer = new();
    private readonly Mock<IBasketQueryService> _basketQueryService = new();
    private readonly Mock<IRepository<CatalogItem>> _itemRepository = new();

    private readonly BasketViewModelService _sut;

    public BasketViewModelServiceTests()
    {
        _sut = new BasketViewModelService(_basketRepository.Object,
            _itemRepository.Object,
            _uriComposer.Object,
            _basketQueryService.Object);
    }

    [Theory]
    [ClassData(typeof(GetBasketData))]
    public async void GetBasketForUser(Basket basket)
    {
        // Arrange
        _basketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
            .ReturnsAsync(basket);

        // Act
        var result = await _sut.GetOrCreateBasketForUser(It.IsAny<string>());

        // Assert
        Assert.Equal(result.BuyerId, basket.BuyerId);
    }

    [Theory]
    [InlineData("user1")]
    [InlineData("user2")]
    [InlineData("user3")]
    [InlineData("user4")]
    [InlineData("user5")]
    public async void CreateBasketForUser(string userName)
    {
        // Arrange
        _basketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
            .ReturnsAsync((Basket)null);

        // Act
        var result = await _sut.GetOrCreateBasketForUser(userName);

        // Assert
        Assert.Equal(result.BuyerId, userName);
    }
}
