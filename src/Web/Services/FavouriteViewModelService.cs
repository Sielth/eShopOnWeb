using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services;

public class FavouriteViewModelService : IFavouritesViewModelService
{
    private readonly IFavouriteQueryService _favouriteQueryService;
    private readonly IRepository<Favourite> _favouriteRepository;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUriComposer _uriComposer;
    private ILogger<FavouriteViewModelService> _logger;

    public FavouriteViewModelService(IFavouriteQueryService favouriteQueryService, IRepository<Favourite> favouriteRepository, IRepository<CatalogItem> itemRepository, IUriComposer uriComposer, ILogger<FavouriteViewModelService> logger)
    {
        _favouriteQueryService = favouriteQueryService;
        _favouriteRepository = favouriteRepository;
        _itemRepository = itemRepository;
        _uriComposer = uriComposer;
        _logger = logger;
    }
    
    public async Task<FavouriteViewModel> GetOrCreateFavouriteForUser(string username)
    {
        var favouriteSpec = new FavouriteWithItemsSpecification(username);
        _logger.LogInformation(favouriteSpec.ToString());
        var favourite = await _favouriteRepository.FirstOrDefaultAsync(favouriteSpec);

        if (favourite == null)
        {
            return await CreateFavouriteForUser(username);
        }
        var viewModel = await Map(favourite);
        return viewModel;
    }

    private async Task<FavouriteViewModel> CreateFavouriteForUser(string userId)
    {
        var favourite = new Favourite(userId);
        await _favouriteRepository.AddAsync(favourite);

        return new FavouriteViewModel()
        {
            BuyerId = favourite.BuyerId,
            Id = favourite.Id,
        };
    }
    
    private async Task<List<FavouriteItemViewModel>> GetFavouriteItems(IReadOnlyCollection<FavouriteItem> favouriteItems)
    {
        var catalogItemsSpecification = new CatalogItemsSpecification(favouriteItems.Select(b => b.CatalogItemId).ToArray());
        var catalogItems = await _itemRepository.ListAsync(catalogItemsSpecification);

        var items = favouriteItems.Select(favouriteItem =>
        {
            _logger.LogInformation($"---> CatalogItemId: {favouriteItem.CatalogItemId}");
            var catalogItem = catalogItems.First(c => c.Id == favouriteItem.CatalogItemId);

            var basketItemViewModel = new FavouriteItemViewModel()
            {
                Id = favouriteItem.Id,
                UnitPrice = favouriteItem.UnitPrice,
                CatalogItemId = favouriteItem.CatalogItemId,
                PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri),
                ProductName = catalogItem.Name
            };
            return basketItemViewModel;
        }).ToList();

        return items;
    }
    
    public async Task<FavouriteViewModel> Map(Favourite favourite)
    {
        return new FavouriteViewModel()
        {
            BuyerId = favourite.BuyerId,
            Id = favourite.Id,
            Items = await GetFavouriteItems(favourite.Items)
        };
    }

    public async Task<int> CountTotalFavouriteItems(string username)
    {
        var counter = await _favouriteQueryService.CountTotalFavourites(username);

        return counter;
    }
}
