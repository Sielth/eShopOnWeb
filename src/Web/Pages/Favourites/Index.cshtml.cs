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
    private readonly ILogger<IndexModel> _logger;
    
    public FavouriteViewModel FavouriteModel { get; set; } = new();

    public IndexModel(IFavouriteService favouriteService, IFavouritesViewModelService favouritesViewModelService, IRepository<CatalogItem> itemRepository, IUsernameHelper usernameHelper, ILogger<IndexModel> logger)
    {
        _favouriteService = favouriteService;
        _favouritesViewModelService = favouritesViewModelService;
        _itemRepository = itemRepository;
        _usernameHelper = usernameHelper;
        _logger = logger;
    }
    
    public async Task OnGet()
    {
        _logger.LogInformation($"---> {nameof(OnGet)} called.");
        FavouriteModel = await _favouritesViewModelService.GetOrCreateFavouriteForUser(_usernameHelper.GetOrSetBasketCookieAndUserName(this));
        _logger.LogInformation($"---> {nameof(FavouriteModel)} with BuyerId: {FavouriteModel.BuyerId}");
    }
    
    public async Task<IActionResult> OnPost(CatalogItemViewModel? productDetails)
    {
        Console.WriteLine("Here in OnPost");
        _logger.LogInformation($"---> {nameof(OnPost)} called.");

        // Id will never be null - TODO: change CatalogItemViewModel Id from int to int?
        _logger.LogInformation($"---> ProductId: {productDetails?.Id}.");
        if (productDetails?.Id is null) return RedirectToPage("/Index");

        var item = await _itemRepository.GetByIdAsync(productDetails.Id);
        _logger.LogInformation($"---> ItemId: {item?.Id}.");
        if (item is null) return RedirectToPage("/Index");

        var username = _usernameHelper.GetOrSetBasketCookieAndUserName(this);
        _logger.LogInformation($"---> Username: {username}.");

        var favourite = await _favouriteService.AddToFavourites(username, productDetails.Id, item.Price);
        _logger.LogInformation($"---> FavouriteId: {favourite?.Id}, Count: {favourite?.Items?.Count} CatalogItemId: {favourite?.Items?.FirstOrDefault()?.CatalogItemId}.");

        FavouriteModel = await _favouritesViewModelService.Map(favourite);
        _logger.LogInformation($"---> FavouriteModelId: {FavouriteModel?.Id}.");

        return RedirectToPage();
    }
}
