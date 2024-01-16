using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace CheckedAppProject.DATA.Entities
{
    public class AppUser : IdentityUser
    {
        public string UserSurname { get; set; }
        public int UserAge { get; set; }
        public string UserSex { get; set; }
        public List<ItemList> ItemList { get; set; } = new List<ItemList>();
    }
}