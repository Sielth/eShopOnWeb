using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Shared.Username;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Favourites;

public class IndexModel : PageModel
{
    private readonly IFavouriteService _favouriteService;
    private readonly IFavouritesViewModelService _favouritesViewModelService;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUsernameHelper _usernameHelper;
    
    public FavouritesViewModel FavouriteModel { get; set; } = new();
    public FavouritesViewModel Favourite { get; set; } = new();



    public IndexModel(IFavouriteService favouriteService, IFavouritesViewModelService favouritesViewModelService, IRepository<CatalogItem> itemRepository, IUsernameHelper usernameHelper)
    {
        _favouriteService = favouriteService;
        _favouritesViewModelService = favouritesViewModelService;
        _itemRepository = itemRepository;
        _usernameHelper = usernameHelper;
    }
    
    public async Task OnGet()
    {
        Favourite = await _favouritesViewModelService.GetOrCreateFavouriteForUser(_usernameHelper.GetOrSetBasketCookieAndUserName(this));
    }
    
    public async Task<IActionResult> OnPost(CatalogItemViewModel? productDetails)
    {
        // Id will never be null - TODO: change CatalogItemViewModel Id from int to int? 
        if (productDetails?.Id == null) return RedirectToPage("/Index");

        var item = await _itemRepository.GetByIdAsync(productDetails.Id);
        if (item is null) return RedirectToPage("/Index");

        var username = _usernameHelper.GetOrSetBasketCookieAndUserName(this);

        var favourites = await _favouriteService.AddToFavourites(username, productDetails.Id, item.Price);
        
        FavouriteModel = await _favouritesViewModelService.Map(favourites, default);

        return RedirectToPage();
    }
}
