namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class BasketViewModel
{
    public int Id { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new();
    public string? BuyerId { get; set; }
    public decimal ItemsTotalPrice { get; set; }

    public decimal CalculateItemsTotalPrice()
    {
        return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
    }

    public decimal CalculateDelivery()
    {
        var itemsTotalPrice = CalculateItemsTotalPrice();
        return itemsTotalPrice >= 300 ? 0m : 300m;
    }
    public decimal CalculateTotal()
    {
        return CalculateItemsTotalPrice() + CalculateDelivery();
    }
}
