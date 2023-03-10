using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services;

public class FavouritesViewModelService : IFavouritesViewModelService
{
    private readonly IFavouriteQueryService _favouriteQueryService;

    public FavouritesViewModelService(IFavouriteQueryService favouriteQueryService)
    {
        _favouriteQueryService = favouriteQueryService;
    }
    public Task<FavouritesViewModel> Map(Favourite favourite, CancellationToken cancellationToken)
    {
        return null;
        // TODO: implement
    }
    public async Task<int> CountTotalFavourites(string username)
    {
        var counter = await _favouriteQueryService.CountTotalFavourites(username);

        return counter;
    }

    public Task<FavouritesViewModel> GetOrCreateFavouriteForUser(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountTotalFavouriteItems(string username)
    {
        throw new NotImplementedException();
    }

    public Task<FavouritesViewModel> Map(Favourite favourite)
    {
        throw new NotImplementedException();
    }
}
