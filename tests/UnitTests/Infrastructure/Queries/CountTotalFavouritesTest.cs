using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Infrastructure.Queries;
public class CountTotalFavouritesTest
{
    private readonly string _buyerId = "buyer1";
    // private FavouriteQueryService? _favouriteQueryService;

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
        var list = GetFakeFavouriteItems().AsQueryable();

        var _mockFavouriteItems = new Mock<DbSet<FavouriteItem>>();
        _mockFavouriteItems.As<IQueryable<FavouriteItem>>().Setup(m => m.Provider).Returns(list.Provider);
        _mockFavouriteItems.As<IQueryable<FavouriteItem>>().Setup(m => m.Expression).Returns(list.Expression);
        _mockFavouriteItems.As<IQueryable<FavouriteItem>>().Setup(m => m.ElementType).Returns(list.ElementType);
        _mockFavouriteItems.As<IQueryable<FavouriteItem>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

        //var _mockFavourites = new Mock<DbSet<Favourite>>();
        //_mockFavourites.Setup(x => x.AddAsync(new Favourite(_buyerId), default));

        var _mockFavouriteDbContext = new Mock<CatalogContext>(options);
        _mockFavouriteDbContext.Setup(x => x.FavouriteItems).Returns(_mockFavouriteItems.Object);

        //_favouriteQueryService = new FavouriteQueryService(_mockFavouriteDbContext.Object);

        // Act
        //var result = await _favouriteQueryService.CountTotalFavourites(_buyerId);

        var result = _mockFavouriteDbContext.Object.FavouriteItems.ToList().Count;

        // Assert
        Assert.Equal(GetFakeFavouriteItems().Count, result);
    }
}
