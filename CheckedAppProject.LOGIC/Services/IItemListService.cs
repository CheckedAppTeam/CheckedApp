using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        int Create(CreateItemListDTO dto);
        IEnumerable<ItemListDTO> GetAll();
        ItemListDTO GetById(int id);
        bool Update(int id, UpdateItemListDTO dto);
        bool Delete(int id);
    }
}