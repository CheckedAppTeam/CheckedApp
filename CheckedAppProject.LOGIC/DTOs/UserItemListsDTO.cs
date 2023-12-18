using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserItemListsDTO
    {
        public List<ItemList> OwnItemList {  get; set; }
        public string UserName { get; set; }
        public bool UserLogged { get; set; }
    }
}
