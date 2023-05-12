using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Infrastructure.Queries;

// TODO: KEY: EXAM => Mock Test EFCore
// https://github.com/MichalJankowskii/Moq.EntityFrameworkCore ReturnsDbSet

public class CountTotalFavouritesTest
{
    private readonly Mock<CatalogContext> _mockCatalogDbContext = new(new DbContextOptions<CatalogContext>());
    private readonly FavouriteQueryService? _favouriteQueryService;

    public CountTotalFavouritesTest()
    {
        _favouriteQueryService = new FavouriteQueryService(_mockCatalogDbContext.Object);
    }
    
    [Fact]
    public async Task TotalCountTest()
    {
        // Arrange
        var buyerId = "Test buyerId 1";
        var favourites = Data.GetFakeFavourite();
        
        _mockCatalogDbContext.Setup(x => x.Favourites).ReturnsDbSet(favourites);
        
        // Act
        var result = await _favouriteQueryService?.CountTotalFavourites(buyerId);

        // Assert
        Assert.Equal(Data.GetFakeFavouriteItems().ToList().Count, result);
    }
}
