using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services;

public class FavouritesViewModelService : IFavouritesViewModelService
{
    private readonly IFavouriteQueryService _favouriteQueryService;
    private readonly IRepository<Favourite> _favouriteRepository;
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUriComposer _uriComposer;




    public FavouritesViewModelService(IFavouriteQueryService favouriteQueryService, IRepository<Favourite> favouriteRepository, IRepository<CatalogItem> itemRepository, IUriComposer uriComposer)
    {
        _favouriteQueryService = favouriteQueryService;
        _favouriteRepository = favouriteRepository;
        _itemRepository = itemRepository;
        _uriComposer = uriComposer;
    }
    public async Task<FavouritesViewModel> Map(Favourite favourite, CancellationToken cancellationToken)
    {
        return new FavouritesViewModel()
        {
            BuyerId = favourite.BuyerId,
            Id = favourite.Id,
            Items = await GetFavouriteItems(favourite.Items)
        };
    }

    public Task<BasketViewModel> GetOrCreateBasketForUser(string username)
    {
        throw new NotImplementedException();
    }

    private async Task<List<FavouriteItemViewModel>> GetFavouriteItems(IReadOnlyCollection<FavouriteItem> favouriteItems)
    {
        var catalogItemsSpecification = new CatalogItemsSpecification(favouriteItems.Select(b => b.CatalogItemId).ToArray());
        var catalogItems = await _itemRepository.ListAsync(catalogItemsSpecification);

        var items = favouriteItems.Select(favouriteItem =>
        {
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

    public async Task<FavouritesViewModel> GetOrCreateFavouriteForUser(string username)
    {
        var favouriteSpec = new FavouriteWithItemsSpecification(username);
        var favourite = (await _favouriteRepository.FirstOrDefaultAsync(favouriteSpec));

        if (favourite == null)
        {
            return await CreateFavouriteForUser(username);
        }
        var viewModel = await Map(favourite, default);
        return viewModel;
        
    }
    
    private async Task<FavouritesViewModel> CreateFavouriteForUser(string userId)
    {
        var favourite = new Favourite(userId);
        await _favouriteRepository.AddAsync(favourite);

        return new FavouritesViewModel()
        {
            BuyerId = favourite.BuyerId,
            Id = favourite.Id,
        };
    }

    public async Task<int> CountTotalFavourites(string username)
    {
        var counter = await _favouriteQueryService.CountTotalFavourites(username);

        return counter;
    }

    public Task<FavouritesViewModel> GetOrCreateFavouriteForUser(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountTotalFavouriteItems(string username)
    {
        throw new NotImplementedException();
    }

    public Task<FavouritesViewModel> Map(Favourite favourite)
    {
        throw new NotImplementedException();
    }
}

public class FavouriteWithItemsSpecification : Specification<Favourite>, ISingleResultSpecification
{
    public FavouriteWithItemsSpecification(string userName)
    {
        Query
            .Where(b => b.BuyerId == userName)
            .Include(b => b.Items);
        
    }
}
