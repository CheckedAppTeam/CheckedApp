using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        Task<ItemList> CopyAsync(int itemListid, int userId);
        Task CreateAsync(CreateItemListDTO dto, int userId);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ItemListDTO>> GetAllAsync();
        Task<IEnumerable<ItemListDTO>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<ItemListDTO>> GetByCityAsync(string city);
        Task<ItemListDTO> GetByIdAsync(int itemListId);
        Task<IEnumerable<ItemListDTO>> GetByMonthAndCity(DateTime date, string city);
        Task<bool> UpdateAsync(UpdateItemListDTO dto, int id);
    }
}