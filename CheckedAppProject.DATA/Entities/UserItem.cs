
namespace CheckedAppProject.DATA.Entities
{
    public class UserItem
    {
        public int UserItemId { get; set; }
        public ItemList ItemList { get; set; }
        public int ItemListId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public UserItemState ItemState { get; set; }
    }
}
