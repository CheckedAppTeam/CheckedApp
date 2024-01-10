using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
namespace CheckedAppProject.DATA.Entities
{
    public class User : IdentityUser
    {
        public string UserSurname { get; set; }
        public int UserAge { get; set; }
        public string UserSex { get; set; }
        public List<ItemList> ItemList { get; set; } = new List<ItemList>();
    }
}
