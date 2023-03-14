using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IFavouritesViewModelService
{
    Task<FavouriteViewModel> GetOrCreateFavouriteForUser(string userName);
    Task<int> CountTotalFavouriteItems(string username);
    Task<FavouriteViewModel> Map(Favourite favourite);
}
