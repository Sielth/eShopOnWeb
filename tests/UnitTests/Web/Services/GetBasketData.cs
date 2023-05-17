using System.Collections;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace Microsoft.eShopWeb.UnitTests.Web.Services;

// TODO: KEY: EXAM => ClassData 

public class GetBasketData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new Basket("Buyer1") };
        yield return new object[] { new Basket("Buyer2") };
        yield return new object[] { new Basket("Buyer3") };
        yield return new object[] { new Basket("Buyer4") };    
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
