using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class FavouriteService : IFavouriteService
{
    private readonly IRepository<Favourite> _basketRepository;
    public FavouriteService(IRepository<Favourite> basketRepository)
    {
        _basketRepository = basketRepository;
        // TODO
    }

    public async Task<Favourite> AddToFavourites(string username, int catalogItemId, decimal price)
    {
        return null;
    }
    // TODO
}
