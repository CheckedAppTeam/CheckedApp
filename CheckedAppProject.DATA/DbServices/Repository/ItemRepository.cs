using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.DATA.DbServices.Repository
{
    public class ItemRepository : IItemRepository
        {
            private List<Item> Items { get; set; }

            public ItemRepository()
            {
                Items = new List<Item>();
            }

            public List<Item> GetAllItemList()
            {
                return Items;
            }

            public Item GetItem(string itemName)
            {
                return Items.FirstOrDefault(item => item.ItemName == itemName);
            }

            public void AddItem(string itemName, string itemCompany = null)
            {
                var newItem = new Item
                {
                    ItemName = itemName,
                    ItemCompany = itemCompany
                };

                Items.Add(newItem);
            }

            public void EditItem(string itemName, string newItemName, string newItemCompany = null)
            {
                var itemToEdit = Items.FirstOrDefault(item => item.ItemName == itemName);

                if (itemToEdit != null)
                {
                    itemToEdit.ItemName = newItemName;
                    itemToEdit.ItemCompany = newItemCompany;
                }
                else
                {
                    throw new ArgumentException("Item not found");
                }
            }
        }
}
