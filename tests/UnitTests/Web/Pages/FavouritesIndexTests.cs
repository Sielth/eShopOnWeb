using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Favourites;
using Microsoft.eShopWeb.Web.Pages.Shared.Username;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// GetOrSetBasketCookieAndUserName cannot be tested since it is private
public class FavouritesIndexTests
{
    private readonly Mock<IFavouriteService> _favouriteServiceMock = new();
    private readonly Mock<IFavouritesViewModelService> _favouriteViewModelService = new();
    private readonly Mock<IRepository<CatalogItem>> _itemRepository = new();
    private readonly Mock<IUsernameHelper> _usernameHelper = new();
    private readonly Mock<ILogger<IndexModel>> _logger = new();

    private CatalogItemViewModel? _catalogItemViewModel =
        new() { Id = 1, Name = "name", Price = 10m, PictureUri = "pictureUri" };

    private FavouriteViewModel _favouriteViewModel =
        new() { BuyerId = "buyerId", Id = 1, Items = new List<FavouriteItemViewModel>() };

    private CatalogItem _catalogItem = new CatalogItem(1, 1, "description", "name", 10m, "pictureUri");

    private readonly IndexModel _sut;

    public FavouritesIndexTests()
    {
        _sut = new IndexModel(_favouriteServiceMock.Object, _favouriteViewModelService.Object, _itemRepository.Object, _usernameHelper.Object, _logger.Object);
    }

    // --- 

    [Fact]
    public async Task OnPost_RedirectsToIndexPage_IfProductIdIsNull()
    {
        // Arrange
        _catalogItemViewModel = null;

        // Act
        var result = await _sut.OnPost(_catalogItemViewModel);

        // Assert
        Assert.Equal(typeof(RedirectToPageResult), result.GetType());
        Assert.Equal("/Index", (result as RedirectToPageResult)?.PageName);
    }

    [Fact]
    public async Task OnPost_RedirectsToIndexPage_IfGetItemByIdAsyncReturnsNull()
    {
        // Arrange
        _catalogItemViewModel = new() { Id = 1, Name = "name", Price = 10m, PictureUri = "pictureUri" };
        _itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(null as CatalogItem);

        // Act
        var result = await _sut.OnPost(_catalogItemViewModel);

        // Assert
        Assert.Equal(typeof(RedirectToPageResult), result.GetType());
        Assert.Equal("/Index", (result as RedirectToPageResult)?.PageName);
    }
    
    [Fact]
    public async Task OnPost_AddItemsFavourites_GetsCalledOnce_IfProductDetailsAndItemsAreNotNull()
    {
        // Arrange
        _itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(_catalogItem);
        _usernameHelper.Setup(x => x.GetOrSetBasketCookieAndUserName(It.IsAny<PageModel>())).Returns("username");
        _favouriteServiceMock.Setup(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()));
        
        // Act
        await _sut.OnPost(_catalogItemViewModel);

        // Assert
        _favouriteServiceMock.Verify(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Once);
    }
    
    [Fact]
    public async Task OnPost_AddItemsFavourites_DoesNotGetCalled_IfItemsIsNull()
    {
        // Arrange
        _itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(null as CatalogItem);
        _usernameHelper.Setup(x => x.GetOrSetBasketCookieAndUserName(It.IsAny<PageModel>())).Returns("username");
        _favouriteServiceMock.Setup(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()));
        
        // Act
        await _sut.OnPost(_catalogItemViewModel);

        // Assert
        _favouriteServiceMock.Verify(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
    }
    
    [Fact]
    public async Task OnPost_AddItemsFavourites_DoesNotGetCalled_IfProductDetailsIsNull()
    {
        // Arrange
        _itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(_catalogItem);
        _usernameHelper.Setup(x => x.GetOrSetBasketCookieAndUserName(It.IsAny<PageModel>())).Returns("username");
        _favouriteServiceMock.Setup(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()));
        
        // Act
        await _sut.OnPost(null);

        // Assert
        _favouriteServiceMock.Verify(x => x.AddToFavourites(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
    }
}
