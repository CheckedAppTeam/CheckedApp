using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        Task AddItemAsync(NewItemDTO dto);
        Task<bool> DeleteItemAsync(int itemId);
        Task<bool> EditItemAsync(ItemDTO dto, int itemId);
        Task<Item> GetItemByName(string name);
        Task<IEnumerable<Item>> GetAllItemDtoAsync();
        Task<Item> GetItemById(int id);
    }
}