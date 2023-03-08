using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;

internal class Favourite : BaseEntity, IAggregateRoot
{
    public string BuyerId { get; private set; }

    private readonly List<Favourite> _favouriteitems = new List<Favourite>();
    public IReadOnlyCollection<Favourite> Items => _favouriteitems.AsReadOnly();
    public int Quantity { get; private set; }
    public int CatalogItemId { get; private set; }
    public int TotalItems => _favouriteitems.Sum(i => i.Quantity);
    public int CatalogTypeID { get; private set; }
    public Favourite(string buyerId)
    {
      
        BuyerId = buyerId;
    }
    public void AddItem(int CatalogTypeID, int catalogbrandId, string description , string name, decimal price)
    {
        if (!Items.Any(i => i.CatalogTypeID == catalogTypeId))
        {
            _favouriteitems.Add(new CatalogItem(CatalogTypeID, catalogbrandId, description,name,price));
            return;
        }
        var existingItem = Items.First(i => i.CatalogItemId == catalogItemId);
        
        existingItem.AddQuantity(quantity);
    }
    public void RemoveEmptyItems()
    {
        _favouriteitems.RemoveAll(i => i.Quantity == 0);
    }

    public void SetNewBuyerId(string buyerId)
    {
        BuyerId = buyerId;
    }

}





