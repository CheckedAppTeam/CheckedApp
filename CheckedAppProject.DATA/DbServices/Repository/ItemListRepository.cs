using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.DbServices.Repository;

public class ItemListRepository : IItemListRepository

{
    private UserItemContext _userItemContext;

    public ItemListRepository(UserItemContext userItemContext)
    {
        _userItemContext = userItemContext;
    }

    public async Task<IEnumerable<ItemList>> GetAllItemListsAsync()
    {
        var itemLists = await _userItemContext
            .ItemLists
            .Include(il => il.Items)
            .ToListAsync();

        return itemLists;
    }

    public async Task<IEnumerable<ItemList>> GetAllByUserIdAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery)
    {
        var query = _userItemContext.ItemLists.AsQueryable();

        query = customQuery(query);

        return await query
            .ToListAsync();
    }

    public async Task<ItemList?> GetItemListAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery)
    {
        var query = _userItemContext.ItemLists.AsQueryable();

        query = customQuery(query);

        return await query
                .Include(il => il.Items)
                .FirstOrDefaultAsync();
    }

    public async Task CreateItemList(ItemList itemList)
    {
        try
        {
            _userItemContext.ItemLists.Add(itemList);
            await _userItemContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateItemListAsync(ItemList itemList)
    {
        var dbItemList = await _userItemContext.ItemLists.FirstOrDefaultAsync(il => il.ItemListId == itemList.ItemListId);

        if (dbItemList == null)
        {
            return false;
        }

        dbItemList.ItemListName = itemList.ItemListName ?? itemList.ItemListName;
        dbItemList.ItemListDestination = itemList.ItemListDestination ?? itemList.ItemListDestination;
        dbItemList.ItemListPublic = itemList.ItemListPublic;
        dbItemList.Date = itemList.Date ?? DateTime.Now;

        await _userItemContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery)
    {
        var query = _userItemContext.ItemLists.AsQueryable();

        query = customQuery(query);

        var itemListToDelete = await query.FirstOrDefaultAsync();

        if (itemListToDelete != null)
        {
            _userItemContext.ItemLists.Remove(itemListToDelete);
            await _userItemContext.SaveChangesAsync();
            return true;
        }
        return false;
    }


}

