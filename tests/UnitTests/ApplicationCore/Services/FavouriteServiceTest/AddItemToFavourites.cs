using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.FavouriteServiceTest;
public class AddItemToFavourites
{
    private readonly string _buyerId = "buyer1";
    private readonly Mock<IRepository<Favourite>> _mockFavouriteRepo = new();

    [Fact]
    public async Task DoesFavouriteServiceFetchAnUsersFavourites()
    {
        var favourite = new Favourite(_buyerId);

        _mockFavouriteRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<FavouriteItemsSpecification>(), default)).ReturnsAsync(favourite);

        var favouriteService = new FavouriteService(_mockFavouriteRepo.Object);
        await favouriteService.AddToFavourites(favourite.BuyerId, 1, 50m);

        _mockFavouriteRepo.Verify(x => x.FirstOrDefaultAsync(It.IsAny<FavouriteItemsSpecification>(), default), Times.Once);
    }
}
