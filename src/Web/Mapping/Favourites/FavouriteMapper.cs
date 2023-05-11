using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.Web.Pages.Favourites;
using Microsoft.eShopWeb.Web.Pages.Shared.Components.FavouriteComponent;
using Microsoft.eShopWeb.Web.Services.FavouriteHeplers;
using Favourite = Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate.Favourite;

namespace Microsoft.eShopWeb.Web.Mapping.Favourites;

public interface IFavouriteMapper:IMapping<Favourite, FavouriteViewModel>
{

}

public class FavouriteMapper : IFavouriteMapper 
{
    private readonly IFavouritItemseHelper _favouriteHelper;
    public FavouriteMapper(IFavouritItemseHelper favouriteHelper)
    {
        _favouriteHelper = favouriteHelper;
    }

    public async Task<FavouriteViewModel> Mapto(Favourite source)
    {
        return new FavouriteViewModel()
        {
            BuyerId = source.BuyerId,
            Id = source.Id,
            Items = await _favouriteHelper.GetFavouriteItems(source.Items)
        };
    }
}
