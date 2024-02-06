using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    [TestFixture]
    public class ItemListRepositoryTest
    {
        private UserItemContext _context;
        private ItemListRepository _itemListRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UserItemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;

            _context = new UserItemContext(options);
            _itemListRepository = new ItemListRepository(_context);
        }

        [TearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllItemListsAsync_ReturnsAllItemLists()
        {
            var expectedLists = new List<ItemList>
            {
                new ItemList { ItemListName = "Test List", Items = new List<Item>() },
                new ItemList { ItemListName = "Another List", Items = new List<Item>() }
            };

            await _context.ItemLists.AddRangeAsync(expectedLists);
            await _context.SaveChangesAsync();

            var itemLists = await _itemListRepository.GetAllItemListsAsync();

            Assert.That(itemLists.Count(), Is.EqualTo(expectedLists.Count));
        }

        [Test]
        public async Task GetItemListNameByIdAsync_ReturnsItemList()
        {
            var itemList = new List<ItemList>
            {
                new ItemList { ItemListName = "Test List" },
                new ItemList { ItemListName = "Another List" }
            };
            await _context.ItemLists.AddRangeAsync(itemList);
            await _context.SaveChangesAsync();

            var itemListName = await _itemListRepository.GetItemListNameByIdAsync(
                itemList[1].ItemListId
            );

            Assert.That(itemListName, Is.EqualTo("Another List"));
        }

        [Test]
        public async Task CreateItemList_AddsItemListToDatabase()
        {
            var newList = new ItemList { ItemListName = "Test List" };

            await _itemListRepository.CreateItemList(newList);
            var listInDb = await _context.ItemLists.AnyAsync(il => il.ItemListName == "Test List");

            Assert.That(listInDb, Is.True);
        }

        [Test]
        public async Task UpdateItemListAsync_UpdatesExistingItemList()
        {
            var existingList = new ItemList { ItemListName = "Existing List" };
            await _context.ItemLists.AddAsync(existingList);
            await _context.SaveChangesAsync();

            var updatedList = new ItemList
            {
                ItemListName = "Updated List",
                ItemListId = existingList.ItemListId
            };

            var result = await _itemListRepository.UpdateItemListAsync(
                updatedList,
                existingList.ItemListId
            );

            var updatedItemList = await _context.ItemLists.FindAsync(existingList.ItemListId);
            Assert.That(result, Is.True);
            Assert.That(updatedItemList.ItemListName, Is.EqualTo("Updated List"));
        }

        [Test]
        public async Task GetAllByUserIdAsync_ReturnsFilteredLists()
        {
            var user1ItemLists = new List<ItemList>
            {
                new ItemList { ItemListName = "User1 List 1", UserId = "USerId1" },
                new ItemList { ItemListName = "User1 List 2", UserId = "UserId1" }
            };

            var user2ItemLists = new List<ItemList>
            {
                new ItemList { ItemListName = "User2 List 1", UserId = "UserId2" },
                new ItemList { ItemListName = "User2 List 2", UserId = "UserId2" }
            };

            await _context.ItemLists.AddRangeAsync(user1ItemLists);
            await _context.ItemLists.AddRangeAsync(user2ItemLists);
            await _context.SaveChangesAsync();

            Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery = query =>
                query.Where(il => il.UserId == "UserId2");

            var filteredLists = await _itemListRepository.GetAllByUserIdAsync(customQuery);

            Assert.That(filteredLists.Count(), Is.EqualTo(2));
            Assert.That(filteredLists, Is.All.Matches<ItemList>(il => il.UserId == "UserId2"));
        }
    }
}
