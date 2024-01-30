using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.DATA.Entities
{
    public class UserItemDTO
    {
        public int UserItemId { get; set; }
        public string UserItemName { get; set; }
        public UserItemState ItemState { get; set; }
        public string UserItemListName { get; set; }
    }
}
