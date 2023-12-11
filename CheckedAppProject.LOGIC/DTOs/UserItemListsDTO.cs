using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserItemListsDTO
    {
        public List<ItemList> OwnItemList {  get; set; }
        public string UserName { get; set; }
        public bool UserLogged { get; set; }
    }
}
