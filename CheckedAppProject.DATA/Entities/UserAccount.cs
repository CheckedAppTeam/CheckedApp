
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckedAppProject.DATA.Entities
{
    public class UserAccount
    {
        public AppUser AppUser { get; set; }
        [ForeignKey("AppUser")]
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
