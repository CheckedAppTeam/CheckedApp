namespace CheckedAppProject.DATA.Entities
{
    public class UserItemDTO
    {
        public ItemList ItemList { get; set; }
        public int ItemListId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public string ItemState { get; set; }
    }
}
