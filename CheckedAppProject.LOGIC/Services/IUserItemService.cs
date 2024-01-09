using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserItemService
    {
        Task AddUserItemAsync(AddUserItemDTO userItemData);
        Task<bool> DeleteUserItemAsync(int userItemId);
        Task<List<UserItemDTO>> GetAllUserItemsByDestinationAsync(string destination);
        Task<List<UserItemDTO>> GetAllUserItemsByListAsync(int id);
        Task<List<UserItemDTO>> GetAllUserItemsByStateInItemListAsync(UserItemState state, int id);
        Task<UserItemDTO> GetUserItemAsync(int userItemId);
        Task<bool> UpdateUserItemStateAsync(UserItemState state, int userItemId);
    }
}