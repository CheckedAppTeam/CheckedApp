using CheckedAppProject.DATA;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemListService
    {
        void AddFoundList(int id);
        void EditItemList(int id);
        List<ItemList> GetListsByTravelDestination(string destination);
        string GetMonthName(DateTime time);
        List<Item> GetOneListByName(int id);
        List<Item> ShowItemList(int id);
    }
}