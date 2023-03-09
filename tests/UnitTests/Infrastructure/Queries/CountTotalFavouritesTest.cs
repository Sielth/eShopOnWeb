using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Infrastructure.Queries;
public class CountTotalFavouritesTest
{
    private readonly string _buyerId = "buyer1";
    private FavouriteQueryService? _favouriteQueryService;

    // Arrange Fake FavouriteItems
    private static List<FavouriteItem> GetFakeFavouriteItems()
    {
        return new List<FavouriteItem>()
        {
            new FavouriteItem(1, 50m),
            new FavouriteItem(2, 25m),
            new FavouriteItem(3, 75m),
            new FavouriteItem(4, 100m)
        };
    }
    [Fact]
    public async Task TotalCountTest()
    {
        // Arrange
        var options = new DbContextOptions<CatalogContext>();
        var list = GetFakeFavouriteItems();
        var _mockFavouiteDbContext = new Mock<CatalogContext>(options);
        _mockFavouiteDbContext.Setup(x => x.Set<FavouriteItem>())
            .ReturnsDbSet(GetFakeFavouriteItems());
        _favouriteQueryService = new FavouriteQueryService(_mockFavouiteDbContext.Object);

        // Act
        var result = await _favouriteQueryService.CountTotalFavourites(_buyerId);

        // Assert
        Assert.Equal(GetFakeFavouriteItems().Count, result);
    }
}
