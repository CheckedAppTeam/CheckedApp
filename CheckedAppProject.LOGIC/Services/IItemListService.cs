using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        Task<int> CreateAsync(CreateItemListDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ItemListDTO>> GetAllAsync();
        Task<ItemListDTO> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateItemListDTO dto);
    }
}