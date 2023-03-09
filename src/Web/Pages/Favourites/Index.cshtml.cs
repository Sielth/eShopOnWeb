using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Favourites;

public class IndexModel : PageModel
{
    private readonly IFavouriteService _basketService;
    private readonly IFavouritesViewModelService _basketViewModelService;
    private readonly IRepository<CatalogItem> _itemRepository;

    public IndexModel(IFavouriteService basketService, IFavouritesViewModelService basketViewModelService, IRepository<CatalogItem> itemRepository)
    {
        _basketService = basketService;
        _basketViewModelService = basketViewModelService;
        _itemRepository = itemRepository;
    }
    
    public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
    {
        return null;
    }
}
