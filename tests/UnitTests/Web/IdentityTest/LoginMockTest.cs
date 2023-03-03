using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.IdentityTest;
public class LoginMockTest
{
    private readonly Mock<FakeSignInManager> _mocksignInmanager;
    private readonly ILogger<LoginModel> _logger;
    private readonly IBasketService _basketService;



    public LoginMockTest()
    {
        var userName = "Tomas";
        var password = "123";

        _mocksignInmanager = new Mock<FakeSignInManager>();
        _mocksignInmanager.Setup(x => x.PasswordSignInAsync(userName, password, default, default));


    }


    [Fact]
    public async Task NotReturnNullIfOrdersArePresIent()
    {
        var loginmodel = new LoginModel(_mocksignInmanager.Object, _logger, _basketService);

        
        var result = await loginmodel.OnPostAsync(default);

        Assert.NotNull(result);
    }




}







public class FakeUserManager : UserManager<ApplicationUser>
{
    public FakeUserManager()
        : base(new Mock<IUserStore<ApplicationUser>>().Object,
          new Mock<IOptions<IdentityOptions>>().Object,
          new Mock<IPasswordHasher<ApplicationUser>>().Object,
          new IUserValidator<ApplicationUser>[0],
          new IPasswordValidator<ApplicationUser>[0],
          new Mock<ILookupNormalizer>().Object,
          new Mock<IdentityErrorDescriber>().Object,
          new Mock<IServiceProvider>().Object,
          new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
    { }

    public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public override Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

}
public class FakeSignInManager : SignInManager<ApplicationUser>
{
    public FakeSignInManager() 
            : base(new FakeUserManager(),
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object,
                 new Mock<IUserConfirmation<ApplicationUser>>().Object)
    { }
}
