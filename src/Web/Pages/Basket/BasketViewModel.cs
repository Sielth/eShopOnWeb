namespace Microsoft.eShopWeb.Web.Pages.Basket;

public class BasketViewModel
{   
    public int Id { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new();
    public string? BuyerId { get; set; }

    public decimal _deliveryFees = 300m;

    public decimal CalculateItemsTotalPrice()
    {
        return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
    }

    public decimal CalculateDelivery()
    {
        var itemsTotalPrice = CalculateItemsTotalPrice();
        return itemsTotalPrice >= 300m ? 0m : _deliveryFees;
    }
    public decimal CalculateTotal()
    {
        return CalculateItemsTotalPrice() + CalculateDelivery();
    }

    public int AddPoints()
    {
        var totalWithoutDelivery = CalculateItemsTotalPrice();
        if (totalWithoutDelivery >= 100)
        {
            return (int)Math.Floor(totalWithoutDelivery/10);
        }

        return 0;
    }
}
