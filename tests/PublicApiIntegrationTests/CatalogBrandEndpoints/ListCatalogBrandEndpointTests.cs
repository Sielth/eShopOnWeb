using System.Threading.Tasks;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PublicApiIntegrationTests.CatalogBrandEndpoints;

[TestClass]
public class ListCatalogBrandEndpointTests
{
    [TestMethod]
    public async Task ReturnsCatalogBrandsList()
    {
        var client = ProgramTest.NewClient;
        var response = await client.GetAsync("api/catalog-brands");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<ListCatalogBrandsResponse>();

        Assert.IsNotNull(model);
    }
}
