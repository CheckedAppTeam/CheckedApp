using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class ItemListDTO
    {
        public int ItemListId { get; set; }
        [Required]
        [MaxLength(20)]
        public string ListName { get; set; }
        public string TravelDestination { get; set; }
        public DateTime TravelDate { get; set; }
        public bool IsPublic { get; set; }
        public List<ItemDTO> ItemsDto { get; set; }
    }
}
