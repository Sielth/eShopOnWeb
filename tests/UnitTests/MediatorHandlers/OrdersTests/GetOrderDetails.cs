using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;

public class GetOrderDetails
{
    private readonly Mock<IReadRepository<Order>> _mockOrderRepository;

    public GetOrderDetails()
    {
        var item = new OrderItem(new CatalogItemOrdered(1, "ProductName", "URI"), 10.00m, 10);
        var item2 = new OrderItem(new CatalogItemOrdered(2, "ProductName", "URI"), 1.00m, 100);
        var item3 = new OrderItem(new CatalogItemOrdered(2, "ProductName", "URI"), 20.00m, 10);

        var address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        
        Order order1 = new Order("buyerId", address, new List<OrderItem> { item, item, item });
        Order order2 = new Order("buyerId", address, new List<OrderItem> { item2, item2, item2 });
        Order order3 = new Order("buyerId", address, new List<OrderItem> { item3, item3, item3 });


        _mockOrderRepository = new Mock<IReadRepository<Order>>();
        _mockOrderRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpec>(), default))
            .ReturnsAsync(order1);
    }

    [Fact]
    public async Task NotBeNullIfOrderExists()
    {
        var request = new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 0);

        var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task ReturnsNumberOfItemsInMyOrder()
    {
        var request = new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 0);

        var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(3, result?.OrderItems.Count);
    }

    [Theory]
    [MemberData(nameof(GetOrderDetailsData.Data), MemberType=typeof(GetOrderDetailsData))]
    public async Task ReturnsTotalPriceOfMyOrder(eShopWeb.Web.Features.OrderDetails.GetOrderDetails request, decimal expected)
    {
        var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, result?.Total);
    }
}

public class GetOrderDetailsData
{
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username1", 1), 300 },
        new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username2", 2), 300 },
        new object[] { new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("Username3", 3), 300 },
    };
}
