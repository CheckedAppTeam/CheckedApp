using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.DATA.Models;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        Task AddItemAsync(NewItemDTO dto);
        Task<bool> DeleteItemAsync(int itemId);
        Task<bool> EditItemAsync(ItemDTO dto, int itemId);
        Task<Item> GetItemByName(string name);
        Task<IEnumerable<ItemDTO>> GetAllItemDtoAsync();
        Task<PageResult<ItemDTO>> GetAllItemDtoAsyncPages(ItemsQuery query);
        Task<Item> GetItemById(int id);
    }
}