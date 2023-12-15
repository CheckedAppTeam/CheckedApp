
namespace CheckedAppProject.DATA
{
    public class ItemList
    {
        public int ItemListId { get; set; }
        public string ListName { get; set; }
        public DateTime TravelDate { get; set; }
        public List<UserItem> Items { get; set; }
        public string TravelDestination {  get; set; }
        public bool IsPublic { get; set; }



    }
}
