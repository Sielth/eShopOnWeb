using System.Collections;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;

// TODO: KEY: EXAM  => ClassData

public class GetOrderDetailsData  : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username1", 1), 300 };
        yield return new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username2", 2), 300 };
        yield return new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username3", 3), 300 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
