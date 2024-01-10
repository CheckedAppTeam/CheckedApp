
namespace CheckedAppProject.DATA.Entities
{
    public class ItemList
    {
        public int ItemListId { get; set; }
        public string ItemListName { get; set; }
        public DateTime? Date {  get; set; }
        public bool ItemListPublic { get; set; }
        public string ItemListDestination { get; set; }
        public User User { get; set; }
        public string Id { get; set; }
        public List<UserItem> UserItems { get; set; }
        public List<Item> Items { get; set; }

    }
}
