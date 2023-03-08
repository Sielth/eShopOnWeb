using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Interfaces;

public interface IFavouritesViewModelService
{
    Task<FavouritesViewModel> Map(Favourite favourite, CancellationToken cancellationToken);
}
