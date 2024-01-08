namespace CheckedAppProject.LOGIC.Services

    {
        public interface IItemService
        {
            Task AddItemAsync(string name, string? company);
            Task DeleteItemAsync(string itemName, int itemListId);
            Task EditNameAsync(string itemName);
            Task ToggleItemStateAsync(string itemName, string itemState);
        }
    }

