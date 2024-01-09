using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserItemService : IUserItemService
    {
        private readonly IMapper _mapper;
        private readonly IUserItemRepository _userItemRepository;

        public UserItemService(UserItemContext userItemContext, IMapper mapper, IUserItemRepository userItemRepository)
        {
            _mapper = mapper;
            _userItemRepository = userItemRepository;
        }

        public async Task<UserItemDTO> GetUserItemAsync(int userItemId)
        {
            var userItemFromDb = await _userItemRepository.GetUserItemRepositoryAsync(query => query.Where(u => u.UserItemId == userItemId));

            if (userItemFromDb == null) return null;

            var userItemDto = _mapper.Map<UserItemDTO>(userItemFromDb);
            return userItemDto;
        }

        public async Task<List<UserItemDTO>> GetAllUserItemsByDestinationAsync(string destination)
        {
            var userItemsFromDb = await _userItemRepository.GetAllUserItemAsync(query => query.Where(u => u.ItemList.ItemListDestination == destination));

            if (userItemsFromDb == null) return null;

            var userItemDto = userItemsFromDb.Select(userItem => _mapper.Map<UserItemDTO>(userItem)).ToList();
            return userItemDto;
        }

        public async Task<List<UserItemDTO>> GetAllUserItemsByStateInItemListAsync(UserItemState state, int id)
        {
            var userItemsFromDb = await _userItemRepository.GetAllUserItemAsync(query => query.Where(u => u.UserItemId == id && u.ItemState == state));

            if (userItemsFromDb == null) return null;

            var userItemsDto = userItemsFromDb.Select(userItem => _mapper.Map<UserItemDTO>(userItem)).ToList();
            return userItemsDto;
        }
        public async Task<List<UserItemDTO>> GetAllUserItemsByListAsync(int id)
        {
            var userItemsFromDb = await _userItemRepository.GetAllUserItemAsync(query => query.Where(u => u.ItemList.ItemListId == id));

            if (userItemsFromDb == null) return null;

            var userItemsDto = userItemsFromDb.Select(userItem => _mapper.Map<UserItemDTO>(userItem)).ToList();
            return userItemsDto;
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