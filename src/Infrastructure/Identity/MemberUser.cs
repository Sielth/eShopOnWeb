namespace Microsoft.eShopWeb.Infrastructure.Identity;

// Liskov
// Open Closed
// Single Responsibility
public class MemberUser : ApplicationUser
{
    public int MemberPoints { get; set; }
    public string? OtherInfos { get; set; } // TODO: Define
    
}
