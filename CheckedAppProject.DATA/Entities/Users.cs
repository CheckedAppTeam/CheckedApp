﻿
namespace CheckedAppProject.DATA.Entities
{
    public class Users
    {
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserAge { get; set; }
        public string UserSex { get; set; }
        public bool UserLogged { get; set; }
    }
}
