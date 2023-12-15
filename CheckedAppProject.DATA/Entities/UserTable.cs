
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.DATA.Entities
{
    public class UserTable
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTableId { get; set; } 
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserAge { get; set; }
        public string UserSex { get; set; }
        public bool UserLogged { get; set; }
        public List<ItemListTable> ItemListTable { get; set; } = new List<ItemListTable>();
    }
}
