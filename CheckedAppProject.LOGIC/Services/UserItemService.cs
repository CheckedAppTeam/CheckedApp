using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserItemService : IUserItemService
    {
        private readonly IMapper _mapper;
        private readonly IUserItemRepository _userItemRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly IItemRepository _itemRepository;

        public UserItemService(UserItemContext userItemContext, IMapper mapper, IUserItemRepository userItemRepository, IItemRepository itemRepository, IItemListRepository itemListRepository)
        {
            _mapper = mapper;
            _userItemRepository = userItemRepository;
            _itemRepository = itemRepository;
            _itemListRepository = itemListRepository;
        }

        public async Task<UserItemDTO> GetUserItemAsync(int userItemId)
        {
            var userItemFromDb = await _userItemRepository.GetUserItemRepositoryAsync(query => query.Where(u => u.UserItemId == userItemId));

            if (userItemFromDb == null) return null;
            UserItemDTO userItemDTO = new UserItemDTO()
            {
                UserItemName = await _itemRepository.GetItemNameByIdAsync(userItemFromDb.ItemId),
                UserItemListName = await _itemListRepository.GetItemListNameByIdAsync(userItemFromDb.ItemListId),
                ItemState = userItemFromDb.ItemState,
            };
            return userItemDTO;
        }

        public async Task<List<UserItemDTO>> GetAllUserItemsByStateInItemListAsync(UserItemState state, int itemListId)
        {
            var userItemList = await _userItemRepository.GetAllUserItemFromListAsync(itemListId);

            if (userItemList == null) return null;

            var filteredUserItems = userItemList
                .Where(ui => ui.ItemState == state)
                .ToList();

            if (filteredUserItems == null) return null;

            var convertedToDTO = filteredUserItems
        .Select(async item => new UserItemDTO()
        {
            UserItemName = await _itemRepository.GetItemNameByIdAsync(item.UserItemId),
            ItemState = item.ItemState,
            UserItemListName = await _itemListRepository.GetItemListNameByIdAsync(item.ItemListId),
        })
            .ToList();

            var userItemDTOArray = await Task.WhenAll(convertedToDTO);
            var userItemDTOList = userItemDTOArray.ToList();

            return userItemDTOList;
        }
        public async Task<List<UserItemDTO>> GetAllUserItemsByListAsync(int itemListId)
        {
            var userItemList = await _userItemRepository.GetAllUserItemFromListAsync(itemListId);
            if (userItemList == null) return null;

            var targetList = _mapper.Map<List<UserItemDTO>>(userItemList);

            for(int i = 0; i < userItemList.Count; i++)
            {
                targetList[i].UserItemListName = await _itemListRepository.GetItemListNameByIdAsync(itemListId);
                targetList[i].UserItemName = await _itemRepository.GetItemNameByIdAsync(userItemList[i].ItemId);
            }

            return targetList;
        }

        public async Task AddUserItemAsync(AddUserItemDTO userItemData)
        {
            {
                var user = _mapper.Map<UserItem>(userItemData);
                await _userItemRepository.AddUserItemAsync(user);

            }
        }

        public async Task<bool> UpdateUserItemStateAsync(UserItemState state, int userItemId)
        {
            return await _userItemRepository.EditUserItemData(state, userItemId);
        }

        public async Task<bool> DeleteUserItemAsync(int userItemId)
        {
            return await _userItemRepository.DeleteUserItemAsync(query => query.Where(u => u.UserItemId == userItemId));
        }
    }
}