using CheckedAppProject.API.Controllers;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Test]
        public async Task GetUserData_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _userServiceMock
                .Setup(s => s.GetUserDataDtoAsync(It.IsAny<string>()))
                .ReturnsAsync((UserDataDTO)null);

            var result = await _controller.GetUserData("noOne");

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetUserData_ReturnsOk_WithUserData_WhenUserExists()
        {
            var userData = new UserDataDTO { UserId = "1", UserName = "TestUser" };
            _userServiceMock.Setup(s => s.GetUserDataDtoAsync("1")).ReturnsAsync(userData);

            var result = await _controller.GetUserData("1");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(userData));
        }

        [Test]
        public async Task GetAllUsersData_ReturnsOk_WithUsersData()
        {
            var usersData = new List<UserDataDTO>
            {
                new UserDataDTO { UserId = "1", UserName = "TestUser1" },
                new UserDataDTO { UserId = "2", UserName = "TestUser2" }
            };
            _userServiceMock.Setup(s => s.GetAllUsersDataDtoAsync()).ReturnsAsync(usersData);

            var result = await _controller.GetAllUsersData();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(usersData));
        }

        [Test]
        public async Task EditUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _userServiceMock
                .Setup(s => s.UpdateUser(It.IsAny<UserUpdateDTO>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _controller.EditUser(new UserUpdateDTO(), "noOne");

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task EditUser_ReturnsOk_WhenUserIsUpdated()
        {
            _userServiceMock
                .Setup(s => s.UpdateUser(It.IsAny<UserUpdateDTO>(), "1"))
                .ReturnsAsync(true);

            var result = await _controller.EditUser(new UserUpdateDTO(), "1");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _userServiceMock.Setup(s => s.DeleteUserDataAsync(It.IsAny<string>())).ReturnsAsync(false);

            var result = await _controller.DeleteUser("noOne");

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task DeleteUser_ReturnsOk_WhenUserIsDeleted()
        {
            _userServiceMock.Setup(s => s.DeleteUserDataAsync("1")).ReturnsAsync(true);

            var result = await _controller.DeleteUser("1");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
