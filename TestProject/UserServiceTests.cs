using AutoMapper;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services;
using Moq;


namespace TestProject
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _userService = new UserService(_mapperMock.Object, _userRepositoryMock.Object);
        }

        [Test]
        public async Task GetUserDataDtoAsync_ByUserId_returnUserDataDto()
        {
            var userId = "UserId";
            var user = new AppUser { Id = userId, UserName = "UncleStan" };
            var userDto = new UserDataDTO { UserId = userId, UserName = "UncleStan" };

            _mapperMock.Setup(mapper => mapper.Map<UserDataDTO>(It.IsAny<AppUser>()))
               .Returns(userDto);
            _userRepositoryMock.Setup(rep => rep.GetUserAsync(It.IsAny<Func<IQueryable<AppUser>, IQueryable<AppUser>>>()))
                .ReturnsAsync(user);

            var result = await _userService.GetUserDataDtoAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo(user.UserName));
            Assert.That(result.UserId, Is.EqualTo(userId));
        }

        [Test]
        public async Task GetAllUsersDataDtoAsync_ReturnsAllUsersDataDtos()
        {
            var users = new List<AppUser> 
            { 
                new AppUser { Id = "1", UserName = "User1" }, 
                new AppUser { Id = "2", UserName = "User2" } 
            };
            var userDtos = users.Select(u => new UserDataDTO { UserId = u.Id, UserName = u.UserName }).ToList();

            _userRepositoryMock.Setup(repo => repo.GetAllUsersDataAsync())
                               .ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDataDTO>>(It.IsAny<IEnumerable<AppUser>>()))
                       .Returns(userDtos);

            var result = await _userService.GetAllUsersDataDtoAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Has.Exactly(1).Matches<UserDataDTO>(dto => dto.UserId == "1" && dto.UserName == "User1"));
            Assert.That(result, Has.Exactly(1).Matches<UserDataDTO>(dto => dto.UserId == "2" && dto.UserName == "User2"));
        }
        [Test]
        public async Task DeleteUserDataAsync_ReturnsTrue_WhenUserIsDeletedSuccessfully()
        {
            var userId = "testUserId";

            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(It.IsAny<Func<IQueryable<AppUser>, IQueryable<AppUser>>>()))
                               .ReturnsAsync(true);

            var result = await _userService.DeleteUserDataAsync(userId);

            Assert.That(result, Is.True);
        }
        [Test]
        public async Task UpdateUser_ReturnsTrue_WhenUserDataIsUpdatedSuccessfully()
        {
            var userId = "testUserId";
            var userDto = new UserUpdateDTO { UserName = "UpdatedUser" };
            var user = new AppUser { Id = userId, UserName = "User" };

            _mapperMock.Setup(mapper => mapper.Map<AppUser>(It.IsAny<UserUpdateDTO>()))
                       .Returns(user);
            _userRepositoryMock.Setup(repo => repo.EditUserData(It.IsAny<AppUser>(), userId))
                               .ReturnsAsync(true);

            var result = await _userService.UpdateUser(userDto, userId);

            Assert.That(result, Is.True);
        }
    }

}
