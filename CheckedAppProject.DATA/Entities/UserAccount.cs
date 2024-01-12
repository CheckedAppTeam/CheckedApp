
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.DATA.Entities
{
    public class UserAccount
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        [Key]
        public string UserId { get; set; }
        public string UserAccountName { get; set; }
        public string UserSurname { get; set; }
        public int UserAge { get; set; }
        public string UserSex { get; set; }
        public List<ItemList> ItemList { get; set; } = new List<ItemList>();
    }
}
