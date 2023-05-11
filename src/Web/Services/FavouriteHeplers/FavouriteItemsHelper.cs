using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services.FavouriteHeplers;

public class FavouriteItemsHelper : IFavouritItemseHelper
{
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IFavouriteQueryService _favouriteQueryService;
    public FavouriteItemsHelper(IRepository<CatalogItem> itemRepository, IUriComposer uriComposer, IFavouriteQueryService favouriteQueryService)
    {
        _itemRepository = itemRepository;
        _uriComposer = uriComposer;
        _favouriteQueryService = favouriteQueryService;
    }
    public async Task<List<FavouriteItemViewModel>> GetFavouriteItems(IReadOnlyCollection<FavouriteItem> favouriteItems)
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

    public async Task<int> CountTotalFavouriteItems(string username)
    {
        var counter = await _favouriteQueryService.CountTotalFavourites(username);

        return counter;
    }



}
