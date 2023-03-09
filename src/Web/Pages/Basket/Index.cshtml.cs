using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Shared.Username;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class IndexModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly IBasketViewModelService _basketViewModelService;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly ILogger<IndexModel> _logger;
    private readonly IUsernameHelper _usernameHelper;

    public IndexModel(IBasketService basketService,
        IBasketViewModelService basketViewModelService,
        IRepository<CatalogItem> itemRepository, ILogger<IndexModel> logger)
    {
        _basketService = basketService;
        _basketViewModelService = basketViewModelService;
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public BasketViewModel BasketModel { get; set; } = new();

    public async Task OnGet()
    {
        BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(_usernameHelper.GetOrSetBasketCookieAndUserName(this));
    }
    
    public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
    {
        // Id will never be null - TODO: change CatalogItemViewModel Id from int to int? 
        if (productDetails?.Id == null) 
        {
            return RedirectToPage("/Index");
        }

        var item = await _itemRepository.GetByIdAsync(productDetails.Id);
        if (item == null)
        {
            return RedirectToPage("/Index");
        }

        var username = _usernameHelper.GetOrSetBasketCookieAndUserName(this);

        var basket = await _basketService.AddItemToBasket(username,
            productDetails.Id, item.Price);

        BasketModel = await _basketViewModelService.Map(basket);

        return RedirectToPage();
    }

    public async Task OnPostUpdate(IEnumerable<BasketItemViewModel> items)
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        var basketView = await _basketViewModelService.GetOrCreateBasketForUser(_usernameHelper.GetOrSetBasketCookieAndUserName(this));
        var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
        var basket = await _basketService.SetQuantities(basketView.Id, updateModel);
        BasketModel = await _basketViewModelService.Map(basket);
    }
}
