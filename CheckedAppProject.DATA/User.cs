
namespace CheckedAppProject.DATA
{
    public class User
    {
        public int UserId {  get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public List<ItemList> OwnItemList { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserAge { get; set; }
        public enum UserSex { }
        public bool UserLogged { get; set; }

    }
}
