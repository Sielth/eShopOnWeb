//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.eShopWeb.ApplicationCore.Interfaces;

//namespace Microsoft.eShopWeb.Infrastructure.Data.Queries;
//public class FavouriteQueryService : IFavouriteQueryService
//{
//    private readonly CatalogContext _dbContext;
//    public FavouriteQueryService(CatalogContext dbContext)
//    {
//        _dbContext = dbContext;
//    }
//    public async Task<int> CountTotalFavourites(string username)
//    {
//        var totalCount = _dbContext.Favourites
//            .Where(fav => fav.BuyerId == username)
//            .SelectMany(items => items.Items)
//            .CountAsync();
//        return await totalCount;
//    }
//}
