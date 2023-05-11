using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Mapping.Favourites;
using Microsoft.eShopWeb.Web.Pages.Favourites;

namespace Microsoft.eShopWeb.Web.Services;

public class FavouriteViewModelService : IFavouritesViewModelService
{
   
    private readonly IRepository<Favourite> _favouriteRepository;
    private ILogger<FavouriteViewModelService> _logger;
    private readonly IFavouriteMapper _favouriteMapper;

    public FavouriteViewModelService( IRepository<Favourite> favouriteRepository, ILogger<FavouriteViewModelService> logger, IFavouriteMapper favouriteMapper)
    {
        _favouriteRepository = favouriteRepository;
        _logger = logger;
        _favouriteMapper = favouriteMapper;
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
        var viewModel = await _favouriteMapper.Mapto(favourite);
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

 

}
