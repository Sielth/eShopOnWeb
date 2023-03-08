using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class FavouriteItemsSpecification : Specification<Favourite>, ISingleResultSpecification
{
    public FavouriteItemsSpecification(string buyerId)
    {
        Query
            .Where(favourite => favourite.BuyerId == buyerId)
            .Include(favourite => favourite.Items);
    }
    // TODO
}
