using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs;

public class EditItemDTO
{
    [Required]
    [MaxLength(20)]
    public string ItemName { get; set; }
}
