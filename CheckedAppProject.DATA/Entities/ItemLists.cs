
using System.Diagnostics.Contracts;

namespace CheckedAppProject.DATA.Entities
{
    public class ItemLists
    {
        public int ItemListsId { get; set; }
        public int UserId { get; set; }
        public string ItemListName { get; set; }
        public DateTime Date {  get; set; }
        public bool ItemListPublic { get; set; }
        public string ItemListDestination { get; set; }

    }
}
