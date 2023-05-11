namespace Microsoft.eShopWeb.Web.Mapping;

public interface IMapping <T,U> where T : class where U : class 
{
    public Task<U> Mapto(T source);

}
