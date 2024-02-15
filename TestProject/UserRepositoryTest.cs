using Microsoft.EntityFrameworkCore;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;

namespace CheckedAppProject.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private UserItemContext _context;
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        { 
            var options = new DbContextOptionsBuilder<UserItemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid()) 
                .Options;

            _context = new UserItemContext(options);

            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]
        public async Task GetUserAsync_ReturnsUser_WhenCustomQueryIsApplied()
        {
            var users = new List<AppUser>
            {
                new AppUser { UserName = "TestManekin" },
                new AppUser { UserName = "KolejnyManekin" }
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery = query => query.Where(u => u.UserName == "TestManekin");

            var result = await _userRepository.GetUserAsync(customQuery);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo("TestManekin"));
        }

        [Test]
        public async Task GetAllUsersDataAsync_ReturnsAllUsers()
        {
            await _context.Users.AddRangeAsync(new AppUser { UserName = "TestManekin", UserSurname = "BiednyPies" }, new AppUser { UserName = "TestManekin2" });
            await _context.SaveChangesAsync();

            var result = await _userRepository.GetAllUsersDataAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().UserName, Is.EqualTo("TestManekin"));
            Assert.That(result.First().UserSurname, Is.EqualTo("BiednyPies"));
        }
        [Test]
        public async Task DeleteUserAsync_ReturnsTrue_WhenUserIsDeleted()
        {
            var user = new AppUser { UserName = "TestManekin" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            Func<IQueryable<AppUser>, IQueryable<AppUser>> customQuery = query => query.Where(u => u.UserName == "TestManekin");
            var result = await _userRepository.DeleteUserAsync(customQuery);

            Assert.That(result, Is.True);
            Assert.That(await _context.Users.AnyAsync(u => u.UserName == "TestManekin"), Is.False);
        }

        [Test]
        public async Task EditUserData_ReturnsTrue_WhenUserDataIsEdited()
        {
            var user = new AppUser { UserName = "Name", UserSurname = "Surname", PasswordHash = "Hash", UserAge = 44 };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var updatedUser = new AppUser { UserName = "NewName", UserSurname = "NewSurname", PasswordHash = "NewHash", UserAge = 66 };
            var result = await _userRepository.EditUserData(updatedUser, user.Id);

            Assert.That(result, Is.True);
            var dbUser = await _context.Users.FindAsync(user.Id);
            Assert.That(dbUser.UserName, Is.EqualTo("NewName"));
            Assert.That(dbUser.UserSurname, Is.EqualTo("NewSurname"));
            Assert.That(dbUser.PasswordHash, Is.EqualTo("NewHash"));
            Assert.That(dbUser.UserAge, Is.EqualTo(66));
        }
    }
}
