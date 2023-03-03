using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.OrderServiceTests;
public class CreateOrder
{
    private readonly Mock<IRepository<Order>> _mockOrderRepository;
    private readonly Mock<IUriComposer> _mockUriComposer;
    private readonly Mock<IRepository<Basket>> _mockBasketRepository;
    private readonly Mock<IRepository<CatalogItem>> _mockCatalogItem;
    private readonly IOrderService _orderService;

    public CreateOrder()
    {
        _mockOrderRepository = new Mock<IRepository<Order>>();
        _mockUriComposer = new Mock<IUriComposer>();
        _mockBasketRepository = new Mock<IRepository<Basket>>();
        _mockCatalogItem = new Mock<IRepository<CatalogItem>>();

        _orderService = new OrderService(_mockBasketRepository.Object,
            _mockCatalogItem.Object,
            _mockOrderRepository.Object,
            _mockUriComposer.Object);

        // Create a non-null basket object with some dummy data
        var basket = new Basket("buyer1");
        basket.AddItem(1, 10.0m, 2);
        basket.AddItem(2, 20.0m, 1);

        // Create a list of catalogItems objects with some dummy data
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem(0, 1, "desc1", "name1", 10.0m, "picture1"),
            new CatalogItem(0, 2, "item2", "name2", 20.0m, "picture2")
        };

        _mockBasketRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), CancellationToken.None))
            .ReturnsAsync(basket);
        _mockCatalogItem.Setup(repo => repo.ListAsync(It.IsAny<CatalogItemsSpecification>(), CancellationToken.None))
            .ReturnsAsync(catalogItems);
    }

    // Arrange with MemberData
    public static IEnumerable<object[]> TestData
    {
        get
        {
            // You can use yield return to return each object[] as a single test case
            yield return new object[] { 1, new Address("123 Main St", "Seattle", "WA", "USA", "98101") };
            yield return new object[] { 2, new Address("456 Elm St", "New York", "NY", "USA", "10001") };
        }
    }
    [Theory]
    [MemberData(nameof(TestData))]
    public async Task CreateOrderTest(int basketId, Address shippingAddress)
    {
        // Arrange
        // Act
        // Call CreateOrderAsync method with input parameters
        await _orderService.CreateOrderAsync(basketId, shippingAddress);

        // Assert
        // Verify that AddAsync method was called once with any order object as argument
        _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>(), CancellationToken.None), Times.Once);
    }

}
