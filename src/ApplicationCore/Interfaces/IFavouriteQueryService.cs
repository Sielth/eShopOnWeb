using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IFavouriteQueryService
{
    Task<int> CountTotalFavourites(string username);
}
