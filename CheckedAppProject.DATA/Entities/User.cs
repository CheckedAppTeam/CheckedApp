﻿
namespace CheckedAppProject.DATA.Entities
{
    public class User
    {
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserAge { get; set; }
        public UserSex UserSex { get; set; }
        public bool UserLogged { get; set; }
        public List<ItemList> ItemList { get; set; } = new List<ItemList>();

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
