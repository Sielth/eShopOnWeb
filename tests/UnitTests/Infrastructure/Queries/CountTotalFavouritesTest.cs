using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Infrastructure.Queries;

public class CountTotalFavouritesTest
{
    private readonly Mock<CatalogContext> _mockCatalogDbContext = new(new DbContextOptions<CatalogContext>());
    private readonly FavouriteQueryService? _favouriteQueryService;

    public CountTotalFavouritesTest()
    {
        _favouriteQueryService = new FavouriteQueryService(_mockCatalogDbContext.Object);
    }

    // Arrange list of Favourites
    private static IEnumerable<Favourite> GetFakeFavourite()
    {
        var favourite1 = new Favourite("buyer1");
        var favourite2 = new Favourite("buyer2");

        var favouriteItemsList = GetFakeFavouriteItems().ToList();
        foreach (var item in favouriteItemsList)
        {
            favourite1.AddItem(item.CatalogItemId, item.UnitPrice);
            favourite2.AddItem(item.CatalogItemId, item.UnitPrice);
        }

        yield return favourite1;
        yield return favourite2;
    }

    // Arrange Fake FavouriteItems
    private static IEnumerable<FavouriteItem> GetFakeFavouriteItems()
    {
        yield return new FavouriteItem(1, 50m);
        yield return new FavouriteItem(3, 75m);
        yield return new FavouriteItem(2, 25m);
        yield return new FavouriteItem(4, 100m);
    }

    [Fact]
    public async Task TotalCountTest()
    {
        // Arrange
        var buyerId = "buyer1";
        var favourites = GetFakeFavourite();
        
        // https://github.com/MichalJankowskii/Moq.EntityFrameworkCore ReturnsDbSet
        _mockCatalogDbContext.Setup(x => x.Favourites).ReturnsDbSet(favourites);
        
        // Act
        var result = await _favouriteQueryService?.CountTotalFavourites(buyerId);

        // Assert
        Assert.Equal(GetFakeFavouriteItems().ToList().Count, result);
    }
}
