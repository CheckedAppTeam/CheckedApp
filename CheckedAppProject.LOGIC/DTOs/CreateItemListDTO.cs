using System;
using System.ComponentModel.DataAnnotations;
using CheckedAppProject.DATA.Entities;

namespace CheckedAppProject.LOGIC.DTOs;

public class CreateItemListDTO
{
    [Required]
    [MaxLength(20)]
    public string ItemListName { get; set; }
    public DateTime? Date { get; set; }
    public bool ItemListPublic { get; set; }
    public string ItemListDestination { get; set; }
    //public List<UserItemCreateListDTO> ItemsDTO { get; set; }
}


