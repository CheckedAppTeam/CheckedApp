﻿using Microsoft.AspNetCore.Identity;

namespace CheckedAppProject.DATA.Entities
{
    public class AppUser : IdentityUser
    {
        public string? UserSurname { get; set; }
        public int? UserAge { get; set; }
        public string? UserSex { get; set; }
        public List<ItemList>? ItemList { get; set; } = new List<ItemList>();
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}