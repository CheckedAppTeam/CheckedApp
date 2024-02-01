using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs;

public class NewItemDTO
{
    [Required]
    [MaxLength(16)]
    public string ItemName { get; set; }
}
