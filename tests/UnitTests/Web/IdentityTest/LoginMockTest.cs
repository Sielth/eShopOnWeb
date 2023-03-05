using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.IdentityTest;

public class LoginMockTest
{
    // private readonly Mock<FakeSignInManager> _mocksignInmanager;
    private readonly SignInManager<ApplicationUser> _signInManager = Substitute.For<SignInManager<ApplicationUser>>();
    private readonly ILogger<LoginModel> _logger = Substitute.For<ILogger<LoginModel>>();
    private readonly IBasketService _basketService = Substitute.For<IBasketService>();

    private readonly LoginModel _sut;
    
    public LoginMockTest()
    {
        _sut = new LoginModel(_signInManager, _logger, _basketService);
        // var userName = "Tomas";
        // var password = "123";
        //
        // _mocksignInmanager = new Mock<FakeSignInManager>();
        // _mocksignInmanager.Setup(x => x.PasswordSignInAsync(userName, password, default, default));
    }


    [Fact]
    public async Task NotReturnNullIfOrdersArePresIent()
    {
        // var loginmodel = new LoginModel(_mocksignInmanager.Object, _logger, _basketService);
        // var result = await loginmodel.OnPostAsync(default);

        _sut.Input = new LoginModel.InputModel();
        
        _sut.Input.Email = "email";
        _sut.Input.Password = "passwod";
        
        var result = await _sut.OnPostAsync("default");
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
    {
    }

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
    {
    }
}
