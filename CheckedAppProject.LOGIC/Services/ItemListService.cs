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

        public async Task<ItemListDTO> GetByIdAsync(int itemListId)
        {
            var itemList = await _itemListRepository.GetItemListAsync(query => query.Where(il => il.ItemListId == itemListId));

            if (itemList == null) return null;

            var itemListDto = _mapper.Map<ItemListDTO>(itemList);
            return itemListDto;
        }

        public async Task<ItemListDTO> GetByCityAsync(string city)
        {
            var itemList = await _itemListRepository.GetItemListAsync(query => query.Where(il => il.ItemListDestination == city));

            if (itemList == null) return null;

            var itemListDto = _mapper.Map<ItemListDTO>(itemList);
            return itemListDto;
        }

        //public async Task<ItemListDTO> GetByMonthAndCity(DateTime date, string city)
        //{
        //    var itemList = await _itemListRepository.GetItemListAsync(query => query.Where(il => il.ItemListDestination == city && il.Date.ToString("MMM") == date.ToString("MMMM")));

        //    if (itemList == null) return null;

        //    var itemListDto = _mapper.Map<ItemListDTO>(itemList);
        //    return itemListDto;
        //}

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

        public async Task CreateAsync(CreateItemListDTO dto, int userId)
        {
            var itemList = _mapper.Map<ItemList>(dto);

            itemList.UserId = userId;

            await _itemListRepository.CreateItemList(itemList);
        }

        public async Task<ItemList> CopyAsync(int itemListid, int userId)
        {
            var itemList = await _dbContext
                .ItemLists
                .Include(il => il.Items)
                .FirstOrDefaultAsync(il => il.ItemListId == itemListid);

            if (itemList != null)
            {
                var newItemList = _mapper.Map<CreateItemListDTO>(itemList);

                var createCopy = CreateAsync(newItemList, userId);

                var copyItemList = new ItemList
                {
                    Date = itemList.Date ?? DateTime.Now,
                    ItemListDestination = itemList.ItemListDestination ?? "Destination",
                    UserId = userId,
                    ItemListName = itemList.ItemListName ?? "ItemList",
                    Items = itemList.Items,
                    ItemListPublic = false
                };

                await _dbContext.SaveChangesAsync();

                return copyItemList;
            }
            else
            {
                throw new Exception("User or ItemList not found.");
            }


        }

        public async Task<bool> UpdateAsync(UpdateItemListDTO dto, int id)
        {
            var itemList = _mapper.Map<ItemList>(dto);
            return await _itemListRepository.UpdateItemListAsync(itemList, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _itemListRepository.DeleteAsync(query => query.Where(il => il.ItemListId == id));
        }

    }
}
