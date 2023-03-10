using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.FavouriteAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) {}

    public virtual DbSet<Basket> Baskets { get; set; }
    public virtual DbSet<CatalogItem> CatalogItems { get; set; }
    public virtual DbSet<CatalogBrand> CatalogBrands { get; set; }
    public virtual DbSet<CatalogType> CatalogTypes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<BasketItem> BasketItems { get; set; }
    public virtual DbSet<FavouriteItem> FavouriteItems { get; set; }
    public virtual DbSet<Favourite> Favourites { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
