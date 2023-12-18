
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string? ItemCompany { get; set; }
        public List<ItemList> ItemList { get; set; }

    }
}
