using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserDataDTO
    {
        public List<ItemList> OwnItemList { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public int UserAge { get; set; }
        public string UserCountry { get; set; }
        public string UserSex { get; set; }
        public bool UserLogged { get; set; }
    }
}
