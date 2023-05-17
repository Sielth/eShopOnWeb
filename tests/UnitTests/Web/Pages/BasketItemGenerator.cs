using System.Collections;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => TDD Example

public class BasketItemGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 10,
                    UnitPrice = 10m
                }
            }, 380
        };
        yield return new object[]
        {
            new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 10,
                    UnitPrice = 40m
                }
            }, 320
        };
        yield return new object[]
        {
            new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 1,
                    UnitPrice = 500m
                }
            }, 400
        };
        yield return new object[]
        {
            new List<BasketItemViewModel>
            {
                new()
                {
                    CatalogItemId = 1,
                    Id = 1,
                    OldUnitPrice = 10,
                    PictureUrl = "string",
                    ProductName = "string",
                    Quantity = 1,
                    UnitPrice = 50m
                }
            }, 340
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
