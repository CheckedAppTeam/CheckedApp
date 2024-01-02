using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
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
        private readonly IItemListRepository _itemListRepository;

        public ItemListService(UserItemContext dbContext, IMapper mapper, IUserService userService, IItemListRepository iemListRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _itemListRepository = iemListRepository;
        }

        public async Task<ItemListDTO> GetByIdAsync(int id)
        {
            var itemList = await _itemListRepository.GetItemListAsync(query => query.Where(il => il.ItemListId == id));

            if (itemList == null) return null;

            var itemListDto = _mapper.Map<ItemListDTO>(itemList);
            return itemListDto;
        }

        public async Task<ItemListDTO> GetByCityAsync(string city)
        {
            var itemList = await _itemListRepository.GetItemListAsync(query => query.Where(il => il.ItemListDestination == city));

            //if (itemList == null) return null;

            var itemListDto = _mapper.Map<ItemListDTO>(itemList);
            return itemListDto;
        }

        public async Task<IEnumerable<ItemListDTO>> GetAllAsync()
        {
            var itemLists = await _itemListRepository.GetAllItemListsAsync();

            var itemListsDtos = _mapper.Map<List<ItemListDTO>>(itemLists);

            return itemListsDtos;
        }

        public async Task<IEnumerable<ItemListDTO>> GetAllByUserIdAsync(int userId)
        {
            var itemLists = await _itemListRepository.GetAllByUserIdAsync(query => query.Where(il => il.UserId == userId));

            var itemListsDto = _mapper.Map<IEnumerable<ItemListDTO>>(itemLists);

            return itemListsDto;
        }

        public async Task CreateAsync(CreateItemListDTO dto)
        {
            var itemList = _mapper.Map<ItemList>(dto);
            await _itemListRepository.CreateItemList(itemList);
        }

        public async Task<ItemList> CopyAsync(int itemListid, int userid)
        {
            var currentUser = await _dbContext
                .Users
                .Include(u => u.ItemList)
                .FirstOrDefaultAsync(u => u.UserId == userid);

            var itemList = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefaultAsync(il => il.ItemListId == itemListid);

            //if (itemList is null)
            //{
            //    throw new Exception();
            //}

            var copyItemList = new ItemList
            {
                Date = itemList.Date,
                ItemListDestination = itemList.ItemListDestination,
                UserId = userid,
                ItemListName = itemList.ItemListName,
                Items = itemList.Items,
                ItemListPublic = false
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
            return await _itemListRepository.DeleteAsync(query => query.Where(il => il.ItemListId == id));
        }

    }
}
