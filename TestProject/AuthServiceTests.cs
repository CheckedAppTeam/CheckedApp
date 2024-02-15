using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace TestProject
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<UserManager<AppUser>> _userManagerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);

            _tokenServiceMock = new Mock<ITokenService>();

            _authService = new AuthService(_userManagerMock.Object, _tokenServiceMock.Object);
        }

        [Test]
        public async Task RegisterAsync_ReturnsSuccess_WhenUserIsCreated()
        {
            var addUserDto = new AddUserDTO
            {
                UserName = "testUser",
                Password = "password",
                UserEmail = "test@example.com"
            };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _authService.RegisterAsync(addUserDto);

            Assert.That(result.Success, Is.True);
        }

        [Test]
        public async Task RegisterAsync_ReturnsFailure_WhenUserCreationFails()
        {
            var addUserDto = new AddUserDTO
            {
                UserName = "testUser",
                Password = "password",
                UserEmail = "test@example.com"
            };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "TestError", Description = "Test error" }));

            var result = await _authService.RegisterAsync(addUserDto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.ErrorMessages, Contains.Key("TestError"));
            Assert.That(result.ErrorMessages["TestError"], Is.EqualTo("Test error"));
        }
    }
}
