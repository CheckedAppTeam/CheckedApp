using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserUpdateDTO
    {
        [MaxLength(30)]
        public string? UserName { get; set; }
        [MaxLength(30)]
        public string? UserSurname { get; set; }
        [Range(0, 100)]
        public int? UserAge { get; set; }
    }
}
