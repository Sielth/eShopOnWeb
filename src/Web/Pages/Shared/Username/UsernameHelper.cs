using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Username;

public class UsernameHelper : IUsernameHelper
{
    public string GetOrSetBasketCookieAndUserName(PageModel pageModel)
    {
        Guard.Against.Null(pageModel.Request.HttpContext.User.Identity, nameof(pageModel.Request.HttpContext.User.Identity));
        string? userName = null;

        if (pageModel.Request.HttpContext.User.Identity.IsAuthenticated)
        {
            Guard.Against.Null(pageModel.Request.HttpContext.User.Identity.Name, nameof(pageModel.Request.HttpContext.User.Identity.Name));
            return pageModel.Request.HttpContext.User.Identity.Name!;
        }

        if (pageModel.Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
        {
            userName = pageModel.Request.Cookies[Constants.BASKET_COOKIENAME];

            if (!pageModel.Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Guid.TryParse(userName, out var _))
                {
                    userName = null;
                }
            }
        }

        if (userName != null) return userName;

        userName = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions { IsEssential = true };
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        pageModel.Response.Cookies.Append(Constants.BASKET_COOKIENAME, userName, cookieOptions);

        return userName;
    }
}
