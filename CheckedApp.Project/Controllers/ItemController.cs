using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    public class ItemController : ControllerBase
    {
        public void DeleteItem(int id) { }
        public void EditItemName(int id, string name) { }
        public void EditBoolValue(int id, bool value) { }
    }
}
