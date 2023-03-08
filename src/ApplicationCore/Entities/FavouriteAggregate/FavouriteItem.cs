using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
internal class FavouriteItem :BaseEntity
{

    public decimal UnitPrice { get; private set; }
   
    public int CatalogItemId { get; private set; }
    public int FavouriteId { get; private set; }

    public FavouriteItem(int catalogItemId, decimal unitPrice)
    {
        CatalogItemId = catalogItemId;
        UnitPrice = unitPrice;
      
    }



}
