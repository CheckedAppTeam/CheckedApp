using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs;

public class UpdateItemListDTO
{
    [Required]
    [MaxLength(20)]
    public string ItemListName { get; set; }
    public DateTime? Date { get; set; }
    public bool ItemListPublic { get; set; }
    public string ItemListDestination { get; set; }
}
