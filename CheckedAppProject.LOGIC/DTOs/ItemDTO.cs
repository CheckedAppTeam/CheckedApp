using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs;

public class ItemDTO
{
    [Required]
    [MaxLength(20)]
    public string ItemName { get; set; }
    public string? ItemCompany { get; set; }
}


