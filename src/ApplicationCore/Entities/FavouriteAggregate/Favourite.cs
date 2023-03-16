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

    private readonly List<FavouriteItem> _items = new();
    public IReadOnlyCollection<FavouriteItem> Items => _items.AsReadOnly();
    public int Quantity { get; private set; }
    public int CatalogItemId { get; private set; }
  
    public int CatalogTypeID { get; private set; }
    public Favourite(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int catalogItemId, decimal price)
    {
        if (Items.Any(i => i.CatalogItemId == catalogItemId) is false)
        {
            _items.Add(new FavouriteItem(catalogItemId, price));
            return;
        }
    }
   
    public void SetNewBuyerId(string buyerId)
    {
        BuyerId = buyerId;
    }

}





