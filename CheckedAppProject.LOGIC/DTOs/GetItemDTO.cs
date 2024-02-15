using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs;

public class GetItemDTO
{
    [Required]
    [MaxLength(20)]
    public string ItemName { get; set; }
    public int ItemId { get; set; }
    public string? ItemCompany { get; set; }
}
