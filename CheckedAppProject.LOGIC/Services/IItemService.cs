namespace CheckedAppProject.LOGIC.Services
{
    public interface IItemService
    {
        void DeleteItem(string itemName, int itemListId);
        void EditName(string itemName);
        void ToggleItemToBuy(string itemName);
        void ToggleItemToPack(string itemName);
    }
}