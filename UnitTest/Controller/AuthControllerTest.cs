using AutoMapper;
using Common.Dto.Auth;
using Common.Exceptions;
using Common.MappingProfiles;
using Domain;
using Microsoft.Extensions.ObjectPool;
using Moq;
using NUnit.Framework;
using PaymentRecorder.Controllers.Auth;
using Service.Abstract.Auth;

namespace UnitTest.Controller
{
    [TestFixture]
    public class AuthControllerTest
    {
        private IEnumerable<ApplicationUser> _users;

        private IAuthService _authService;
        private ITokenService _tokenService;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _users = new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Id = "1ebaa77f-e0db-4901-8c4d-038dc57e2345",
                    UserName = "alexandr.chioroglo",
                    NormalizedUserName = "ALEXANDR.CHIOROGLO",
                    Email = "alexandr.chioroglo@office.com",
                    NormalizedEmail = "ALEXANDR.CHIOROGLO@OFFICE.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "uZO/ULtUZWgnUM/RRZG3l901DsuZBKTjuO1DiXCWItxLT1DnaEO+/1m4EpZZXzyUluYNB1+A/QWq11i01Ohf5A==",
                    SecurityStamp = "JVDSZ6DIADMDGGUYH3GCISABY65AGYHZ",
                    ConcurrencyStamp = "32ec7101-1b6d-4022-96a6-8117444e78cf",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    Firstname = "Alexandr",
                    Lastname = "Chioroglo",
                    RefreshToken =
                        "CP1niaDdEZpcBpGmL1bZreplH898Y9dnL9z8E5jXC9I1fBWS1NILDMRiCfvSRKWxk4FhdX0k3voxpZOgVS9RLez2oZVWEO4ch0omMZUmyxvytSuZaIeBSZ_LctHnTlfL",
                    RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(1)
                },
                new ApplicationUser
                {
                    Id = "f66eb30d-1cfa-4d49-a630-c0e3a8b815e4",
                    UserName = "vitalie.calasnicov",
                    NormalizedUserName = "VITALIE.CALASNICOV",
                    Email = "vitalie.calasnicov@office.com",
                    NormalizedEmail = "VITALIE.CALASNICOV@OFFICE.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "uZO/ULtUZWgnUM/RRZG3l901DsuZBKTjuO1DiXCWItxLT1DnaEO+/1m4EpZZXzyUluYNB1+A/QWq11i01Ohf5A==",
                    SecurityStamp = "C7TT7XOGPTNNNA6WLQED3CNJFENHSGJW",
                    ConcurrencyStamp = "2cb1e97e-c7d2-462a-9aa6-c6f6afe52122",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    Firstname = "Vitalie",
                    Lastname = "Calașnicov",
                    RefreshToken =
                        "AMiEvE7LIVO17pSrBohk9KHnzYR+JbMTlJOkm1QWnQOH8UMklgakhwgLH5xhlLjSxSaheOlKhwHSfAyD1SJWsYYlk+xioV2gjR+t                            ",
                    RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(1)
                }
            };
            _mapper = new Mapper(new MapperConfiguration(opt => opt.AddProfile<UserProfile>()));
        }

        [Test]
        public async Task RegistrationFailsWhenEmailAlreadyExists()
        {
            var authServiceMock = new Mock<IAuthService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var dto = new RegistrationDto
            {
                Firstname = "Vitalie",
                Lastname = "Calașnicov",
                Username = "vitalie.calasnicov",
                Email = "vitalie.calasnicov@office.com",
                Password = "test_pwd1__"
            };
            authServiceMock.Setup(x => x.RegisterAsync(dto, default)).Throws(new IdentityException("aaa"));
            var controller = new AuthController(authServiceMock.Object, tokenServiceMock.Object, _mapper);

            Assert.ThrowsAsync<IdentityException>(async () => await controller.AddNewUserAsync(dto, default));

            //Assert.That(await controller.AddNewUserAsync(dto, default),);
        }
    }
}