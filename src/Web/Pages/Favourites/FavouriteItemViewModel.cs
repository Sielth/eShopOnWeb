namespace Microsoft.eShopWeb.Web.Pages.Favourites;

public class FavouriteItemViewModel
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string? PictureUrl { get; set; }
}
