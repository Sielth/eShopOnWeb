## Tests done:

### [eShopOnWeb github repo](https://github.com/sielth/eShopOnWeb)

1. [tests/UnitTests/ApplicationCore/Services/OrderServiceTests/CreateOrder.cs](https://github.com/Sielth/eShopOnWeb/blob/main/tests/UnitTests/ApplicationCore/Services/OrderServiceTests/CreateOrder.cs) => MemberData + Mocking
2. [tests/UnitTests/Web/Services/BasketViewModelServiceTests.cs](https://github.com/Sielth/eShopOnWeb/blob/main/tests/UnitTests/Web/Services/BasketViewModelServiceTests.cs) => MemberData + InlineData + Mocking
3. [tests/UnitTests/Web/IdentityTest/LoginMockTest.cs](https://github.com/Sielth/eShopOnWeb/blob/main/tests/UnitTests/Web/IdentityTest/LoginMockTest.cs) => Vi brugte virkelig lang tid til at teste login funktionen, dog uden held, da SignInManager har ikke et interface, derfor kan det ikke mockes.
4. [tests/UnitTests/MediatorHandlers/OrdersTests/GetOrderDetails.cs](https://github.com/Sielth/eShopOnWeb/blob/main/tests/UnitTests/MediatorHandlers/OrdersTests/GetOrderDetails.cs) => ClassData + Mocking

## Assignment 

I jeres projekt: eshopOnWeb skal der oprettes eksempler på hvor og hvordan I vil brugt DDT. Der skal laves både Theory -> inline, Class, member og mocking.

Framework til mocking er valgfrit.

Der skal laves mindst 3 eksempler på DDT og mocking i projektet.

Lav det evt. på jeres nye features som i er blevet enige om i gruppen at lave.

### Ekstra
Prøv at måle hastighedsforskellen ved brug af mocking ift. testdata hentet fra DB.