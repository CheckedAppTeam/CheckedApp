using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.DATA.Models;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        Task AddItemAsync(NewItemDTO dto);
        Task<bool> DeleteItemAsync(int itemId);
        Task<bool> EditItemAsync(EditItemDTO dto, int itemId);
        Task<Item> GetItemByName(string name);
        Task<IEnumerable<GetItemDTO>> GetAllItemDtoAsync();
        Task<PageResult<GetItemDTO>> GetAllItemDtoAsyncPages(ItemsQuery query);
        Task<Item> GetItemById(int id);
    }
}