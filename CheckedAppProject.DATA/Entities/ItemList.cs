﻿namespace CheckedAppProject.DATA.Entities
{
    public class ItemList
    {
        public int ItemListId { get; set; }
        public string ItemListName { get; set; }
        public DateTime? Date { get; set; }
        public bool ItemListPublic { get; set; }
        public string ItemListDestination { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public List<UserItem> UserItems { get; set; }
        public List<Item> Items { get; set; }
    }
}
