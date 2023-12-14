﻿using CheckedAppProject.DATA;
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserDataDTO
    {
        public int UserId { get; set; }
        public List<ItemListDTO> OwnItemList { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        [Required]
        [Range(0, 100)] 
        public int UserAge { get; set; }
        public string UserSex { get; set; }
    }
}
