using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CheckedAppProject.LOGIC.Services
{
    public class ItemListService : IItemListService
    {
        private readonly UserItemContext _dbContext;
        private IMapper _mapper;
        private readonly IUserService _userService;

        public ItemListService(UserItemContext dbContext, IMapper mapper, IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ItemListDTO> GetByIdAsync(int id)
        {
            var itemList = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefaultAsync(il => il.ItemListId == id);

            if (itemList is null) return null;

            var result = _mapper.Map<ItemListDTO>(itemList);
            return result;
        }

        public async Task<ItemListDTO> GetByCityAsync(string city)
        {
            var itemList = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefaultAsync(il => il.ItemListDestination == city);

            if (itemList is null) return null;

            var result = _mapper.Map<ItemListDTO>(itemList);
            return result;
        }

        public async Task<IEnumerable<ItemListDTO>> GetAllAsync()
        {
            var itemLists = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .ToListAsync();

            var itemListsDto = _mapper.Map<IEnumerable<ItemListDTO>>(itemLists);

            return itemListsDto;
        }

        public async Task<IEnumerable<ItemListDTO>> GetAllByUserIdAsync(User user)
        {
            var currentUser = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            var itemLists = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .Include(il => il.UserId == currentUser.UserId)
                .ToListAsync();

            var itemListsDto = _mapper.Map<IEnumerable<ItemListDTO>>(itemLists);

            return itemListsDto;
        }

        public async Task<int> CreateAsync(CreateItemListDTO dto)
        {
            var itemList = _mapper.Map<ItemList>(dto);
            _dbContext.ItemLists.Add(itemList);
            await _dbContext.SaveChangesAsync();

            return itemList.ItemListId;
        }

        public async Task<ItemList> CopyAsync(int itemListid, User user)
        {
            var currentUser = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            var itemList = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefaultAsync(il => il.ItemListId == itemListid);

            var copyItemList = new ItemList
            {
                Date = itemList.Date,
                ItemListDestination = itemList.ItemListDestination,
                User = user,
                UserId = user.UserId,
                ItemListName = itemList.ItemListName,
                Items = itemList.Items,
                ItemListPublic = false,
                ItemListId = _dbContext.ItemLists.Count() + 1

            };

            currentUser.ItemList.Add(copyItemList);

            await _dbContext.SaveChangesAsync();

            return copyItemList;

        }

        public async Task<bool> UpdateAsync(int id, UpdateItemListDTO dto)
        {
            var itemList = await _dbContext
                .ItemLists
                .FirstOrDefaultAsync(il => il.ItemListId == id);
            //.ExecuteUpdate(setters => setters.SetProperty(b => b.Rating, b => b.Rating + 1));

            if (itemList == null)
            {
                return false;
            }

            itemList.ItemListName = dto.ItemListName;
            itemList.ItemListDestination = dto.ItemListDestination;
            itemList.ItemListPublic = dto.ItemListPublic;
            itemList.Date = dto.Date;

            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var itemList = await _dbContext
                .ItemLists
                .FirstOrDefaultAsync(il => il.ItemListId == id);

            if (itemList is null) return false;

            _dbContext.ItemLists.Remove(itemList);
            _dbContext.SaveChanges();

            return true;
        }


        //public List<Item> ShowItemList(int id)
        //{
        //    return null;
        //}
        //public void EditItemList(int id) { }
        //public List<Item> GetOneListByName(int id) 
        //{
        //    return null;
        //}
        //public List<ItemList> GetListsByTravelDestination(string destination) 
        //{
        //    return null;
        //}
        //public void AddFoundList(int id) { }
        //public string GetMonthName(DateTime time)
        //{
        //    string monthName = time.ToString("MMMM");
        //    return monthName;
        //}
    }
}
