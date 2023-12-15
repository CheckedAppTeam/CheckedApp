
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class AddUserDTO
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(40)]
        public string UserSurname { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Range(0, 100)]
        public int UserAge { get; set; }
        public enum UserSex { }
    }
}
