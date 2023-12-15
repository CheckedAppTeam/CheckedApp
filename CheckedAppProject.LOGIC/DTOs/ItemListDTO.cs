using CheckedAppProject.DATA;


namespace CheckedAppProject.LOGIC.DTOs
{
    public class ItemListDTO
    {
        public int ItemListId { get; set; }
        public string ListName { get; set; }
        public string TravelDestination { get; set; }
        public DateTime TravelDate {  get; set; }
        public bool IsPublic { get; set; }
    }
}
