
namespace CheckedAppProject.DATA.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string? ItemCompany { get; set; }
        public List<UserItem> UserItemList { get; set; }
        public List<ItemList> ItemLists { get; set; }

    }
}
