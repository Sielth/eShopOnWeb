using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IFavouriteService
{
    Task<Favourite> AddToFavourites(string username, int catalogItemId, decimal price);
    // TODO
}
