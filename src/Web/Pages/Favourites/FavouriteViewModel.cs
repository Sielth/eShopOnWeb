﻿namespace Microsoft.eShopWeb.Web.Pages.Favourites;

public class FavouriteViewModel
{
    public int Id { get; set; }
    public List<FavouriteItemViewModel> Items { get; set; } = new();
    public string? BuyerId { get; set; }
    
}
