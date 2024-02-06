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
        [Test]
        public async Task GetAllItemsAsync_ReturnsAllItems()
        {
            var expectedItems = new List<Item>
            {
                new Item { ItemName = "TestItem", ItemCompany = "TestCompany" },
                new Item { ItemName = "TestItem1", ItemCompany = "TestCompany2" }
            };

            await _contextIR.Items.AddRangeAsync(expectedItems);
            await _contextIR.SaveChangesAsync();

            var items = await _itemRepository.GetAllItemsAsync();

            Assert.That(items.Count(), Is.EqualTo(expectedItems.Count));
        }
        [Test]
        public async Task GetItemNameByIdAsync_ReturnsItemName()
        {
            var newItem = new Item { ItemName = "TestItem", ItemCompany = "TestCompany" };
            await _contextIR.Items.AddAsync(newItem);
            await _contextIR.SaveChangesAsync();

            var itemName = await _itemRepository.GetItemNameByIdAsync(newItem.ItemId);

            Assert.That(itemName, Is.EqualTo(newItem.ItemName));
        }
        [Test]
        public async Task GetItemByNameAsync_ReturnsCorrectItem()
        {
            var newItem1 = new Item { ItemName = "TestItem", ItemCompany = "TestCompany1" };
            var newItem2 = new Item { ItemName = "TestItem2", ItemCompany = "TestCompany2" };
            await _contextIR.Items.AddRangeAsync(new List<Item> { newItem1, newItem2 });
            await _contextIR.SaveChangesAsync();

            var item = await _itemRepository.GetItemByNameAsync("TestItem2");

            Assert.That(item, Is.Not.Null);
            Assert.That(item.ItemCompany, Is.EqualTo(newItem2.ItemCompany));
        }
        [Test]
        public async Task DeleteItemAsync_ReturnsTrue_WhenItemIsDeleted()
        {
            var newItem = new Item { ItemName = "TestItem", ItemCompany = "TestCompany" };
            await _contextIR.Items.AddAsync(newItem);
            await _contextIR.SaveChangesAsync();

            Func<IQueryable<Item>, IQueryable<Item>> customQuery = (items) => items.Where(i => i.ItemId == newItem.ItemId);

            var result = await _itemRepository.DeleteItemAsync(customQuery);

            var itemInDb = await _contextIR.Items.AnyAsync(i => i.ItemId == newItem.ItemId);
            Assert.That(result, Is.True);
            Assert.That(itemInDb, Is.False);
        }

    }
}
