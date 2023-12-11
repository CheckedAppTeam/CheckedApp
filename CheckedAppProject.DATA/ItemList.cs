
namespace CheckedAppProject.DATA
{
    public class ItemList
    {
        public string ListName { get; set; }
        public DateTime TravelDate { get; set; }
        public List<UserItem> Items { get; set; }
        public string TravelDestination {  get; set; }
        private int ItemListId { get; set; }
        private bool IsPublic { get; set; }



    }
}
