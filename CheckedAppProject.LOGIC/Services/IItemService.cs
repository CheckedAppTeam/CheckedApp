using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        Task AddItemAsync(ItemDTO dto);
        Task DeleteItemAsync(string itemName, int itemListId);
        Task EditNameAsync(string itemName);
        Task ToggleItemStateAsync(string itemName, string itemState);
    }
}