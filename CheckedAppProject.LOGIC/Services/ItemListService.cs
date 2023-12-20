using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.Services
{
    public class ItemListService : IItemListService
    {
        public List<Item> ShowItemList(int id)
        {
            return null;
        }
        public void EditItemList(int id) { }
        public List<Item> GetOneListByName(int id) 
        {
            return null;
        }
        public List<ItemList> GetListsByTravelDestination(string destination) 
        {
            return null;
        }
        public void AddFoundList(int id) { }
        public string GetMonthName(DateTime time)
        {
            string monthName = time.ToString("MMMM");
            return monthName;
        }
    }
}
