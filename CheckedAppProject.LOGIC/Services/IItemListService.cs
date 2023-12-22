using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        int Create(CreateItemListDTO dto);
        IEnumerable<ItemListDTO> GetAll();
        ItemListDTO GetById(int id);
        bool Delete(int id);
    }
}