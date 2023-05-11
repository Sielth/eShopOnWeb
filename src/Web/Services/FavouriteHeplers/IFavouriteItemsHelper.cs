using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services.FavouriteHeplers;

public interface IFavouritItemseHelper
{   
    
    Task<List<FavouriteItemViewModel>> GetFavouriteItems(IReadOnlyCollection<FavouriteItem> favouriteItems);
    Task<int> CountTotalFavouriteItems(string username);

}
