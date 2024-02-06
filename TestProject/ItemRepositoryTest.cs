using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using Microsoft.EntityFrameworkCore;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject
{
    [TestFixture]
    public class ItemRepositoryTest
    {
        private UserItemContext _contextIR;
        private ItemRepository _itemRepository;
        private Mock<ILogger<ItemRepository>> _loggerMock;

        [SetUp]

        public void Setup ()
        {
            var options = new DbContextOptionsBuilder<UserItemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseIR" + Guid.NewGuid())
                .Options;

            _contextIR = new UserItemContext(options);
            _loggerMock = new Mock<ILogger<ItemRepository>>();

            _itemRepository = new ItemRepository(_contextIR, _loggerMock.Object);
        }

        [TearDown]
        public void CleanUp()
        {
            _contextIR.Database.EnsureDeleted();
            _contextIR.Dispose();
        }

        [Test]
        public async Task AddItemToRepository_ReturnsTrue_WhenNewItemIsAdded()
        {
            var newItem = new Item { ItemName = "TestItem", ItemCompany = "TestCompany" };

            await _itemRepository.AddItemAsync(newItem);

            var newItemInDb = await _contextIR.Items.AnyAsync(i => i.ItemName == "TestItem" && i.ItemCompany == "TestCompany");
            Assert.That(newItemInDb, Is.True, "newItem have not been added");
        }
        [Test]
        public async Task EditItem_ReturnsEditedItem_WhenItemIsEdited()
        {
            var newItem = new Item { ItemName = "TestItem", ItemCompany = "TestCompany" };
            await _contextIR.Items.AddAsync(newItem);
            await _contextIR.SaveChangesAsync();

            Item updatedItem = new Item { ItemName = "UpdatedName", ItemCompany = "UpdatedCompany" };
            var result = await _itemRepository.EditItemAsync(updatedItem, newItem.ItemId);

            Assert.That(result, Is.True);
            var dbItem = await _contextIR.Items.FindAsync(newItem.ItemId);
            Assert.That(dbItem.ItemName, Is.EqualTo("UpdatedName"));
            Assert.That(dbItem.ItemCompany, Is.EqualTo("UpdatedCompany"));
        }
    }
}
