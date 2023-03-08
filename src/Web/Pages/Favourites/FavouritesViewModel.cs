namespace Microsoft.eShopWeb.Web.Pages.Favourites;

public class FavouritesViewModel
{
    public int Id { get; set; }
    public List<FavouritesViewModel> Items { get; set; } = new();
    public string? BuyerId { get; set; }
}
