using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        Task CreateAsync(CreateItemListDTO dto, int userId);
        Task<ItemListDTO> GetByCityAsync(string city);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ItemListDTO>> GetAllAsync();
        Task<IEnumerable<ItemListDTO>> GetAllByUserIdAsync(int userid);
        Task<ItemListDTO> GetByIdAsync(int id);
        Task<ItemList> CopyAsync(int itemListid, int userid);
        Task<bool> UpdateAsync(UpdateItemListDTO dto, int id);
    }
}