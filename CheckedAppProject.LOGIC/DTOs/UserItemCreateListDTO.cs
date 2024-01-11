using System;
using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.DTOs;

public class UserItemCreateListDTO
{
    public string UserItemName { get; set; }
    public UserItemState ItemState { get; set; }
    public string? CompanyName { get; set; }
}


