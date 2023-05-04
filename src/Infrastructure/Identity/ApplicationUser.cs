using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public bool? MembershipPlus { get; set; }
    public MemberInfo? MemberInfo { get; set; }
}

public class MemberInfo
{
    public int MemberPoints { get; set; }
    public string CreditCardInfo { get; set; } // TODO: Define
}
