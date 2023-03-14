using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class FavouriteWithItemsSpecification : Specification<Favourite>, ISingleResultSpecification
{
    public FavouriteWithItemsSpecification(string userName)
    {
        Query
            .Where(b => b.BuyerId == userName)
            .Include(b => b.Items);
        
    }
}
