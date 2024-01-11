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
        var expectedUser = new UserAccount { Id = 1, UserAccountName = "TestUser" };
        var mockDbSet = new Mock<DbSet<UserAccount>>();
        var data = new List<UserAccount> { expectedUser }.AsQueryable();

        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Provider).Returns(data.Provider);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Expression).Returns(data.Expression);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext.Setup(c => c.UsersApp).Returns(mockDbSet.Object);

        Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery = q => q.Where(u => u.Id == 1);

        var result = await _userRepository.GetUserAsync(customQuery);

        Assert.That(result, Is.EqualTo(expectedUser));
    }

    [Test]
    public async Task GetAllUsersDataAsync_ShouldReturnAllUsersWithData()
    {
        var expectedUsers = new List<UserAccount>
        {
            new UserAccount { Id = 1, UserAccountName = "User1" },
            new UserAccount { Id = 2, UserAccountName = "User2" }
        };

        var mockDbSet = new Mock<DbSet<UserAccount>>();
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Provider).Returns(() => expectedUsers.AsQueryable().Provider);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Expression).Returns(() => expectedUsers.AsQueryable().Expression);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.ElementType).Returns(() => expectedUsers.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.GetEnumerator()).Returns(() => expectedUsers.AsQueryable().GetEnumerator());

        _mockContext.Setup(c => c.UsersApp).Returns(mockDbSet.Object);

        var result = await _userRepository.GetAllUsersDataAsync();

        
        Assert.That(result, Is.EquivalentTo(expectedUsers));
    }

    [Test]
    public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserExistsAndIsDeleted()
    {      
        var userToDelete = new UserAccount { Id = 1, UserAccountName = "UserToDelete" };

        var mockDbSet = new Mock<DbSet<UserAccount>>();
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Provider).Returns(() => new List<UserAccount> { userToDelete }.AsQueryable().Provider);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.Expression).Returns(() => new List<UserAccount> { userToDelete }.AsQueryable().Expression);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.ElementType).Returns(() => new List<UserAccount> { userToDelete }.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<UserAccount>>().Setup(m => m.GetEnumerator()).Returns(() => new List<UserAccount> { userToDelete }.AsQueryable().GetEnumerator());

        _mockContext.Setup(c => c.UsersApp).Returns(mockDbSet.Object);

        Func<IQueryable<UserAccount>, IQueryable<UserAccount>> customQuery = q => q.Where(u => u.Id == 1);

        var result = await _userRepository.DeleteUserAsync(customQuery);

        Assert.That(result, Is.True);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

   
}