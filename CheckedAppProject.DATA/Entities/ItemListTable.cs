
using System.Diagnostics.Contracts;

namespace CheckedAppProject.DATA.Entities
{
    public class ItemListTable
    {
        public int ItemListTableId { get; set; }
        public string ItemListName { get; set; }
        public DateTime Date {  get; set; }
        public bool ItemListPublic { get; set; }
        public string ItemListDestination { get; set; }

        public UserTable UserTable { get; set; }
        public int UserTableId { get; set; }//może tego nie być EF sam by bez tego to wymyślił
    }
}
