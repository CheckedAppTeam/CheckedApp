using CheckedAppProject.DATA;
using Microsoft.AspNetCore.Mvc;


namespace CheckedAppProject.API.Controllers
{
    public class ItemListController : ControllerBase
    { 
        public void AddList(string name) { }
        public List<Item> GetList() { return null; }
        public List<List<Item>> GetAllLists() { return null; }
        public void UpdateList(List<Item> list) { }
        public void DeleteList(string name) { }
        public void PublishList(string name) { }
        public void TakeDownList(string name) { }
        public List<string> GetAllListNames() { return null; }
    }
}
