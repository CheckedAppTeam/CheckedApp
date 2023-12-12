
namespace CheckedAppProject.DATA.Entities
{
    public class UserItemTable
    {
        public ItemListTable ItemListTable { get; set; }
        public int ItemListTableId { get; set; }
        public ItemTable ItemTable { get; set; }
        public int ItemTableId { get; set; }
        public string ItemState { get; set; }
    }
}
