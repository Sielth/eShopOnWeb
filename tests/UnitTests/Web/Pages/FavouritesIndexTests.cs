using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Favourites;
using Microsoft.eShopWeb.Web.ViewModels;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

public class FavouritesIndexTests
{
    private readonly Mock<IFavouriteService> _favouriteServiceMock = new();
    private readonly Mock<IFavouritesViewModelService> _favouriteViewModelService = new();
    private readonly Mock<IRepository<CatalogItem>> _itemRepository = new();

    private readonly IndexModel _sut;

    public FavouritesIndexTests()
    {
        _sut = new IndexModel(_favouriteServiceMock.Object, _favouriteViewModelService.Object, _itemRepository.Object);
    }

    [Fact]
    public async Task OnPost_AddItemsToBasket_GetsCalledOnce()
    {
        var id = 1;
        var catalogViewModel = new CatalogItemViewModel { Id = id };
        var favouriteViewModel = new FavouritesViewModel();
        var catalogItem = new CatalogItem(1,1,"desc", "name", 10m, "picture");

        _itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(catalogItem);
        
        // username might fail?
        _favouriteViewModelService.Setup(x => x.Map(It.IsAny<Favourite>(), default))
            .ReturnsAsync(favouriteViewModel);

        _favouriteServiceMock.Verify(service => service.AddToFavourites("username", 1, 1));

    }

}
