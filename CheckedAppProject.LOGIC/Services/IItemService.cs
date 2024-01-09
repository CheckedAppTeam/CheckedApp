using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        Task AddItemAsync(ItemDTO dto);
        Task<bool> DeleteItemAsync(int itemId);
        Task<bool> EditItemAsync(ItemDTO dto, int itemId);
        Task<IEnumerable<Item>> GetAllItemDtoAsync();
        Task<Item> GetItemById(int id);
    }
}