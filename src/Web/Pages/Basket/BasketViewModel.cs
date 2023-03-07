namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class BasketViewModel
{
    public int Id { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
    public string? BuyerId { get; set; }
    public decimal Delivery { get; set; } = 300m;

    public decimal Total()
    {
        var total = Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);

        if (total >= Delivery) Delivery = 0;
        
        return total += Delivery;
    }
}
