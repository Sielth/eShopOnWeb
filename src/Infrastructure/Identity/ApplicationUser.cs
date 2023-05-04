using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    
}

// Liskov
// Open Closed
// Single Responsibility
public class MemberUser : ApplicationUser
{
    public int MemberPoints { get; set; }
    public string CreditCardInfo { get; set; } // TODO: Define
}
