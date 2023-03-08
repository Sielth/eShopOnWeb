using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services;

public class FavouritesViewModelService : IFavouritesViewModelService
{
    public Task<FavouritesViewModel> Map(Favourite favourite, CancellationToken cancellationToken)
    {
        return null;
        // TODO: implement
    }
}
