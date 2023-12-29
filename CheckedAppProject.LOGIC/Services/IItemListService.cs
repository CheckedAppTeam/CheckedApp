using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        Task<int> CreateAsync(CreateItemListDTO dto);
        Task<ItemListDTO> GetByCityAsync(string city);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ItemListDTO>> GetAllAsync();
        Task<IEnumerable<ItemListDTO>> GetAllByUserIdAsync(User user);
        Task<ItemListDTO> GetByIdAsync(int id);
        Task<ItemList> CopyAsync(int itemListid, User user);
        Task<bool> UpdateAsync(int id, UpdateItemListDTO dto);
    }
}