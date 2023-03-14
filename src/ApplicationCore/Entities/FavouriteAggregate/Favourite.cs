using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;

public class Favourite : BaseEntity, IAggregateRoot
{
    public string BuyerId { get; private set; }

    private readonly List<FavouriteItem> _favouriteitems = new List<FavouriteItem>();
    public IReadOnlyCollection<FavouriteItem> Items => _favouriteitems.AsReadOnly();
    public int Quantity { get; private set; }
    public int CatalogItemId { get; private set; }
  
    public int CatalogTypeID { get; private set; }
    public Favourite(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int catalogItemID, decimal price)
    {
        if (!Items.Any(i => i.CatalogItemId == catalogItemID))
        {
            _favouriteitems.Add(new FavouriteItem(CatalogTypeID, price));
            return;
        }
    }
   
    public void SetNewBuyerId(string buyerId)
    {
        BuyerId = buyerId;
    }

}





