using CheckedAppProject.DATA;
using Microsoft.AspNetCore.Mvc;
using CheckedAppProject.LOGIC.Services.Logger;
using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.API.Controllers
{
    public class ItemListController : ControllerBase
    {
        private readonly IAppLogger _logger ;
        public ItemListController(IAppLogger logger)
        {
            _logger = logger;
        }

        public void AddList(string name) 
        {
            try
            {
                _logger.LogToFileAndConsole("Successfuly added a list.");
                
                
            }catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }
        public List<Item> GetList() { return null; }
        public List<List<Item>> GetAllLists() { return null; }
        public void UpdateList(List<Item> list) { }
        public void DeleteList(string name) { }
        public void PublishList(string name) { }
        public void TakeDownList(string name) { }
        public List<string> GetAllListNames() { return null; }
    }
}
