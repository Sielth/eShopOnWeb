using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Username;

public interface IUsernameHelper
{
    string GetOrSetBasketCookieAndUserName(PageModel pageModel);
}
