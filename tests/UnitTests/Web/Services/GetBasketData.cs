using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.UnitTests.Web.Services;

// TODO: KEY: EXAM => MemberData 

public static class GetBasketData
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new Basket("Buyer1") },
            new object[] { new Basket("Buyer2") },
            new object[] { new Basket("Buyer3") },
            new object[] { new Basket("Buyer4") },
        };
}
