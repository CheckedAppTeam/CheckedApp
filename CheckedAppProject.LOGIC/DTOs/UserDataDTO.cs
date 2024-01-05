using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserDataDTO
    {
        [Required]
        public int UserId { get; set; }
        public List<ItemListDTO> OwnItemList { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserSurname { get; set; }
        [Required]
        [Range(0, 100)] 
        public int UserAge { get; set; }
        [MaxLength(30)]
        public string UserSex { get; set; }
    }
}
