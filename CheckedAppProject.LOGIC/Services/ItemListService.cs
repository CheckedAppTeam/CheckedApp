using CheckedAppProject.DATA;

namespace CheckedAppProject.LOGIC.Services
{
    public class ItemListService : IItemListService
    {
        public List<Item> ShowItemList(int id)
        {

        }
        public void EditItemList(int id) { }
        public List<Item> GetOneListByName(int id) { }
        public List<ItemList> GetListsByTravelDestination(string destination) { }
        public void AddFoundList(int id) { }
        public string GetMonthName(DateTime time)
        {
            string monthName = time.ToString("MMMM");
            return monthName;
        }
    }
}
