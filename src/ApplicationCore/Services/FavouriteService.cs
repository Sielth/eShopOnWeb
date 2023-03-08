using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class FavouriteService : IFavouriteService
{
    private readonly IRepository<Favourite> _favouriteRepository;
    public FavouriteService(IRepository<Favourite> favouriteRepository)
    {
        _favouriteRepository = favouriteRepository;
        // TODO
    }

    public async Task<Favourite> AddToFavourites(string username, int catalogItemId, decimal price)
    {
        var favouritesSpec = new FavouriteItemsSpecification(username);
        var favourites = await _favouriteRepository.FirstOrDefaultAsync(favouritesSpec);

        if (favourites is null)
        {
            favourites = new Favourite(username);
            await _favouriteRepository.AddAsync(favourites);
        }
        
        favourites.AddItem(catalogItemId, price);

        await _favouriteRepository.UpdateAsync(favourites);
        return favourites;
    }
    // TODO
}
