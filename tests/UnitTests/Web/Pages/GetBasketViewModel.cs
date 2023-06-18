using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.UnitTests.Web.Pages;

// TODO: KEY: EXAM => MemberData

public static class GetBasketViewModel
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new List<BasketItemViewModel>
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
            }, 300 },
            new object[] { new List<BasketItemViewModel>
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
            }, 0 },            
            new object[] { new List<BasketItemViewModel>
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
            }, 0 },
            new object[] { new List<BasketItemViewModel>
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
            }, 300 },
        };
    
    public static IEnumerable<object[]> PointsData =>
        new List<object[]>
        {
            new object[] { new List<BasketItemViewModel>
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
            }, 10 },
            new object[] { new List<BasketItemViewModel>
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
            }, 40 },            
            new object[] { new List<BasketItemViewModel>
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
            }, 50 },
            new object[] { new List<BasketItemViewModel>
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
            }, 0 },
        };
}
