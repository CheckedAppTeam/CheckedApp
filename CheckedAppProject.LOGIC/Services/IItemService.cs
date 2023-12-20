namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
       
            void AddItem(string name, string? company);
            void DeleteItem(string itemName, int itemListId);
            void EditName(string itemName);
            void ToggleItemState(string itemName, string itemState);

    }
}