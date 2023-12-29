using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.DbServices.Repository;

public class ItemListRepository<T> where T : class
{
    private UserItemContext _userItemContext;

    public ItemListRepository(UserItemContext userItemContext)
    {
        _userItemContext = userItemContext;
    }
    //public async Task<ItemList?> GetItemListAsync(Func<IQueryable<ItemList>, IQueryable<ItemList>> customQuery)
    //{
    //    var query = _userItemContext.ItemLists.AsQueryable();

    //    query = customQuery(query);

    //    return await query
    //        .Include(il => il.Items)
    //        .FirstOrDefaultAsync();
    //}

    //public async Task<IEnumerable<ItemList>> GetAllItemListsAsync()
    //{
    //    var itemLists = await _userItemContext
    //        .ItemLists
    //        .Include(il => il.Items)
    //        .ToListAsync();

    //    return itemLists;
    //}


    //public void Add(T entity)
    //{
    //    _userItemContext.Set<T>(entity);
    //}

}

//public class ItemListRepository
//{
//    public List<ItemList> ItemLists { get; set; }
//}

