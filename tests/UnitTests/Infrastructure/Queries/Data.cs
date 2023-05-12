using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;

namespace Microsoft.eShopWeb.UnitTests.Infrastructure.Queries;

public static class Data
{
    // Arrange list of Favourites
    public static IEnumerable<Favourite> GetFakeFavourite()
    {
        var favourite1 = new Favourite("Test buyerId 1");
        var favourite2 = new Favourite("Test buyerId 2");

        var favouriteItemsList = GetFakeFavouriteItems().ToList();
        foreach (var item in favouriteItemsList)
        {
            favourite1.AddItem(item.CatalogItemId, item.UnitPrice);
            favourite2.AddItem(item.CatalogItemId, item.UnitPrice);
        }

        yield return favourite1;
        yield return favourite2;
    }

    // Arrange Fake FavouriteItems
    public static IEnumerable<FavouriteItem> GetFakeFavouriteItems()
    {
        yield return new FavouriteItem(1, 50m);
        yield return new FavouriteItem(3, 75m);
        yield return new FavouriteItem(2, 25m);
        yield return new FavouriteItem(4, 100m);
    }

}
