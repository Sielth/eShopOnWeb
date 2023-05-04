using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

namespace SpecFlowShop.Specs.Steps;

[Binding]
public class BasketStepDefinitions
{
    private BasketItem? _item;
    private readonly Basket _basket = new("buyer");
    
    [Given(@"an item with id (.*)")]
    public void GivenAnItemWithId(int id)
    {
        _item = new BasketItem(id, 1, 1);
    }

    [When(@"the item is added into the basket")]
    public void WhenTheItemIsAddedIntoTheBasket()
    {
        if (_item is not null) _basket.AddItem(1, 1, 1);
    }

    [Then(@"the basket should contain the item with id (.*)")]
    public void ThenTheBasketShouldContainTheItemWithId(int id)
    {
        _basket.Items.Should().Contain(i => i.Id == id);
    }

    [Given(@"a basket that contains an item with id (.*)")]
    public void GivenABasketThatContainsAnItemWithId(int id)
    {
        _item = _basket.Items.FirstOrDefault(i => i.Id == id);
    }

    [When(@"the item is removed from the basket")]
    public void WhenTheItemIsRemovedFromTheBasket()
    {
        if (_item is not null) _basket.Items.ToList().Remove(_item);
    }

    [Then(@"the basket should not contain the item with id (.*)")]
    public void ThenTheBasketShouldNotContainTheItemWithId(int id)
    {
        _basket.Items.Should().NotContain(i => i.Id == id);
    }
}
