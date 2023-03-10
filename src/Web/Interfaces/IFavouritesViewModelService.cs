using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IFavouritesViewModelService
{
    Task<FavouritesViewModel> Map(Favourite favourite, CancellationToken cancellationToken);

    Task<FavouritesViewModel> GetOrCreateFavouriteForUser(string userName);
    Task<int> CountTotalFavouriteItems(string username);
    Task<FavouritesViewModel> Map(Favourite favourite);
}
