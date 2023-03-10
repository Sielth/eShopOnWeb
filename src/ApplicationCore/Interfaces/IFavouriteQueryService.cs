using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IFavouriteQueryService
{
    Task<int> CountTotalFavourites(string username);
}

public class FavouriteQueryService : IFavouriteQueryService
{
    public Task<int> CountTotalFavourites(string username)
    {
        throw new System.NotImplementedException();
    }
}
