using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;



[TestFixture]
public class UserRepositoryTests
{
    private Mock<UserItemContext> _mockContext;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        _mockContext = new Mock<UserItemContext>();
        _userRepository = new UserRepository(_mockContext.Object);
    }

    [Test]
    public async Task GetUserAsync_ShouldReturnUser_WhenCustomQueryProvided()
    {
        // Arrange
        var expectedUser = new User { UserId = 1, UserName = "TestUser" };
        var mockDbSet = new Mock<DbSet<User>>();
        var data = new List<User> { expectedUser }.AsQueryable();

        mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

        Func<IQueryable<User>, IQueryable<User>> customQuery = q => q.Where(u => u.UserId == 1);

        // Act
        var result = await _userRepository.GetUserAsync(customQuery);

        // Assert
        Assert.That(result, Is.EqualTo(expectedUser));
    }

    [Test]
    public async Task GetAllUsersDataAsync_ShouldReturnAllUsersWithData()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new User { UserId = 1, UserName = "User1" },
            new User { UserId = 2, UserName = "User2" }
        };

        var mockDbSet = new Mock<DbSet<User>>();
        mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => expectedUsers.AsQueryable().Provider);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => expectedUsers.AsQueryable().Expression);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => expectedUsers.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => expectedUsers.AsQueryable().GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

        // Act
        var result = await _userRepository.GetAllUsersDataAsync();

        // Assert
        Assert.That(result, Is.EquivalentTo(expectedUsers));
    }

    [Test]
    public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserExistsAndIsDeleted()
    {
        // Arrange
        var userToDelete = new User { UserId = 1, UserName = "UserToDelete" };

        var mockDbSet = new Mock<DbSet<User>>();
        mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(() => new List<User> { userToDelete }.AsQueryable().Provider);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(() => new List<User> { userToDelete }.AsQueryable().Expression);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(() => new List<User> { userToDelete }.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => new List<User> { userToDelete }.AsQueryable().GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

        Func<IQueryable<User>, IQueryable<User>> customQuery = q => q.Where(u => u.UserId == 1);

        // Act
        var result = await _userRepository.DeleteUserAsync(customQuery);

        // Assert
        Assert.That(result, Is.True);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

   
}